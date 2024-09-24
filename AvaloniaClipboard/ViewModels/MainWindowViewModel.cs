using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaClipboard.Models;
using AvaloniaClipboard.Models.Interfaces;
using AvaloniaClipboard.ViewModels.Observables;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels;

public class MainWindowViewModel : ViewModelBase, IDisposable
{
    public IClipboardHotkeyManager ClipboardHotkeyManager { get; set; }
    private bool _isKeyReading, _isStarted=false;
    private KeyCode _keyCode = KeyCode.VcUndefined;
    public ObservableDoubleHotkey DoubleHotkey { get; set; } = new();
    public ObservableDoubleHotkey CurrentHotkey { get; set; } = new();
    public ObservableCollection<ObservableDoubleHotkey> Hotkeys { get; set; } = [];
    
    public MainWindowViewModel(IClipboard clipboard)
    {
        ClipboardHotkeyManager = new ObservableClipboardHotkeyManager(clipboard);
        this.WhenAnyValue(x => x.IsKeyReading).Subscribe(OnKeyReadingChanged);
        this.WhenAnyValue(x => x.IsStarted).Subscribe(OnStarted);
        this.WhenAnyValue(x => x.KeyReader.CurrentKey).Subscribe(x=>CurrentKey =x);
    }
    

    public IList<KeyCode> PressedKeys
    {
        get => KeyReader.PressedKeys;
        set => KeyReader.PressedKeys = value;
    }
    
    public KeyCode CurrentKey
    {
        get => _keyCode;
        set => this.RaiseAndSetIfChanged(ref _keyCode, value);
    }
    
    public bool IsKeyReading
    {
        get => _isKeyReading;
        set => this.RaiseAndSetIfChanged(ref _isKeyReading, value);
    }
    
    public bool IsStarted
    {
        get => _isStarted;
        set => this.RaiseAndSetIfChanged(ref _isStarted, value);
    }

    public IList<KeyCode> Keys => KeyReader.PressedKeys;

    private ObservableKeyReader KeyReader { get; } = new()
    {
        PressedKeys = new ObservableCollection<KeyCode>()
    };


    public void Dispose()
    {
        KeyReader.Stop();
        KeyReader.Dispose();
        ClipboardHotkeyManager.HotkeyManager.Stop();
        ClipboardHotkeyManager.HotkeyManager.Dispose();
    }

    public void AddNewHotkey()
    {
        foreach (var hotkey in Hotkeys)
        {
            if(hotkey.BoardName == DoubleHotkey.BoardName) return;
        }
        Hotkeys.Add(DoubleHotkey.Copy());
    }
    public void ClearCurrentHotkey()
    {
        DoubleHotkey.BoardName = "";
        DoubleHotkey.KeysToBoard.Clear();
        DoubleHotkey.KeysToClipBoard.Clear();
    }

    public void RemoveCurrentHotkey()
    {
        Hotkeys.Remove(CurrentHotkey);
    }
    private void OnKeyReadingChanged(bool value)
    {
        if (value)
            KeyReader.Start();
        else
            KeyReader.Stop();
    }

    public void Save()
    {
        ClipboardHotkeyManager.Clear();
        foreach (var hotkey in Hotkeys)
        {
            ClipboardHotkeyManager.AddHotkey_SetToBoard(hotkey.KeysToBoard.ToArray(), hotkey.BoardName);
            ClipboardHotkeyManager.AddHotkey_SetToClipBoard(hotkey.KeysToClipBoard.ToArray(), hotkey.BoardName);
        }
    }
    private void OnStarted(bool value)
    {
        if (value)
        {
            ClipboardHotkeyManager.HotkeyManager.Start();
            IsKeyReading = false;
        }
        else
        {
            ClipboardHotkeyManager.HotkeyManager.Stop();
        }
    }

    public void NewKeyToBoard()
    {
        if(CurrentKey == KeyCode.VcUndefined) return;
        DoubleHotkey.KeysToBoard.Add(CurrentKey);
    }

    public void NewKeyToClipBoard()
    {
        if(CurrentKey == KeyCode.VcUndefined) return;
        DoubleHotkey.KeysToClipBoard.Add(CurrentKey);
    }
}