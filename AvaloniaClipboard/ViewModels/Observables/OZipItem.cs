using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Remote.Protocol.Input;
using DynamicData.Binding;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook.Defaults;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels.Observables;

public class OZipItem: ReactiveObject, IDisposable
{
    private IHotkey _hotkey;
    private readonly ObservableCollection<bool> _activated;
    private readonly ZipItems _parent;
    public OZipItem(ZipItems parent, IHotkey hotkey, int index)
    {
        _hotkey = hotkey;
        Index = index;
        _activated = (ObservableCollection<bool>)_hotkey.IsActivated;
        _activated.CollectionChanged += OnCollectionChanged;
        _parent = parent;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        this.RaisePropertyChanged(nameof(Bool));
        _parent.OnPropertyChanged(nameof(_parent.IsActive));
    }

    public int Index { get; set; } = 0;
    public bool Bool => _activated[Index];
    public string Key => _hotkey.KeyCodes[Index].ToString();

    public void Dispose()
    {
        _activated.CollectionChanged -= OnCollectionChanged;
        GC.SuppressFinalize(this);
    }
}

public class ZipItems : ObservableCollection<OZipItem>
{
    public bool IsActive => this.All(x => x.Bool);
    public ZipItems(IHotkey hotkey)
    {
        for (var index = 0; index < hotkey.IsActivated.Count; index++)
        {
            Add(new OZipItem(this, hotkey, index));
        }
    }

    protected override event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}