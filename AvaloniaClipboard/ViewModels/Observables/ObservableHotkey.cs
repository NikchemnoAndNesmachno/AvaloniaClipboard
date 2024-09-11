using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableHotkey : ObservableBase, IHotkey
{
    private int _activatedKeys;
    private KeyCode[] _keyCodes = [];

    public ObservableHotkey()
    {
    }

    public ObservableHotkey(KeyCode[] codes, Action action)
    {
        KeyCodes = codes;
        OnHotkey = action;
        this.Reset();
    }

    public ObservableCollection<bool> IsActivatedObservable { get; set; } = [];

    public int ActivatedKeys
    {
        get => _activatedKeys;
        set => this.RaiseAndSetIfChanged(ref _activatedKeys, value);
    }

    public KeyCode[] KeyCodes
    {
        get => _keyCodes;
        set => this.RaiseAndSetIfChanged(ref _keyCodes, value);
    }

    public bool[] IsActivated { get; set; } = [];

    public Action OnHotkey { get; set; } = () => { };
}