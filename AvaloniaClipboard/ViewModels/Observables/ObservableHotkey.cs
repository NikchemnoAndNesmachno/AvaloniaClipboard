using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Input;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableHotkey : ObservableBase, IHotkey
{
    private int _activatedKeys;
    private IList<KeyCode> _keyCodes = new ObservableCollection<KeyCode>();
    public int ActivatedKeys
    {
        get => _activatedKeys;
        set => this.RaiseAndSetIfChanged(ref _activatedKeys, value);
    }

    public ObservableHotkey()
    {
        
    }

    public IList<bool> IsActivated { get; set; } = new ObservableCollection<bool>();

    public IList<KeyCode> KeyCodes
    {
        get => _keyCodes;
        set
        {
            this.RaiseAndSetIfChanged(ref _keyCodes, value);
            IsActivated = new ObservableCollection<bool>();
            for (int i = 0; i < _keyCodes.Count; i++)
            {
                IsActivated.Add(false);
            }
        }
    } 
    


    public Action OnHotkey { get; set; } = () => { };
}