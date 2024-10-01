using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia.Remote.Protocol.Input;
using DynamicData.Binding;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableZipItem: ObservableBase, IDisposable
{
    private KeyCode _key;
    private bool _isActivated;
    private Action _unRegister;
    public KeyCode Key
    {
        get => _key;
        set => this.RaiseAndSetIfChanged(ref _key, value);
    }
    
    public bool IsActivated
    {
        get => _isActivated;
        set => this.RaiseAndSetIfChanged(ref _isActivated, value);
    }
    private int Index { get; set; }

    public void Register(ObservableCollection<bool> bools, IList<KeyCode> keys, int index)
    {
        Index = index;
        bools.CollectionChanged += OnChanged;
        _unRegister = () => { Unregister(bools); };
        Key = keys[index];
    }

    public void Unregister(ObservableCollection<bool> bools)
    {
        bools.CollectionChanged -= OnChanged;
    }

    private void OnChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (sender is not ObservableCollection<bool> hotkeys) return;
        IsActivated = hotkeys[Index];
    }

    public void Dispose()
    {
        _unRegister();
    }
}