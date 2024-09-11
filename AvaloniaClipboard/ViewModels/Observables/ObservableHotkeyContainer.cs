using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Input;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableHotkeyContainer: ObservableBase, IHotkeyContainer
{
    private bool _isPaused = false;
    private KeyCode[] _pauseHotkey = [];
    
    public void Add(KeyCode[] codes, Action action)
    {
        Hotkeys.Add(new ObservableHotkey(codes, action));
    }

    public void Remove(KeyCode[] codes)
    {
        IHotkey? hotkeyToRemove = null;
        foreach (var hotkey in Hotkeys)
        {
            int i = codes.Count(code => hotkey.KeyCodes.Contains(code));
            if (i != codes.Length) continue;
            hotkeyToRemove = hotkey;
            break;
        }

        if (hotkeyToRemove is null) return;
        Hotkeys.Remove(hotkeyToRemove);
    }

    public KeyCode[] PauseHotkey
    {
        get => _pauseHotkey;
        set => this.RaiseAndSetIfChanged(ref _pauseHotkey, value);
    }
    public IList<IHotkey> Hotkeys { get; set; } = new ObservableCollection<IHotkey>();

    public bool IsPaused
    {
        get => _isPaused;
        set => this.RaiseAndSetIfChanged(ref _isPaused, value);
    }
}