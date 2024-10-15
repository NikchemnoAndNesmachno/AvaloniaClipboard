using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AvaloniaClipboard.Models;
using AvaloniaClipboard.Models.Interfaces;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels.LjsonConverters;
using AvaloniaClipboard.ViewModels.Observables;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;
using IClipboard = Avalonia.Input.Platform.IClipboard;

namespace AvaloniaClipboard.ViewModels;

public class HotkeyViewModel: ViewModelBase, IDisposable
{
    public HotkeyViewModel(IClipboardHotkeyManager hotkeyManager, LayoutSwitcher layoutSwitcher)
    {
        ClipboardHotkeyManager = hotkeyManager;
        LayoutSwitcher = layoutSwitcher;
        this.WhenAnyValue(x => x.IsKeyReading).Subscribe(OnKeyReadingChanged);
        this.WhenAnyValue(x => x.IsStarted).Subscribe(OnStarted);
        this.WhenAnyValue(x => x.KeyReader.CurrentKey).Subscribe(x=>CurrentKey =x);
        layoutSwitcher.WhenAnyValue(x => x.CurrentLayoutIndex).Subscribe(OnCurrentLayoutChanged);
        layoutSwitcher.WhenAnyValue(x => x.CurrentLayout.FilePath).Subscribe(OnFilePathChanged);
    }
    public IClipboardHotkeyManager ClipboardHotkeyManager { get; set; }
    private bool _isKeyReading, _isStarted;
    private string _filePath = "";
    private KeyCode _keyCode = KeyCode.VcUndefined;
    private ObservableCollection<ObservableDoubleHotkey> _hotkeys = [];

    public ObservableCollection<ObservableDoubleHotkey> Hotkeys
    {
        get => _hotkeys;
        set => this.RaiseAndSetIfChanged(ref _hotkeys, value);
    }
    private ObservableKeyReader KeyReader { get; } = new()
    {
        PressedKeys = new ObservableCollection<KeyCode>()
    };
    public ObservableDoubleHotkey DoubleHotkey { get; set; } = new();
    public ObservableDoubleHotkey CurrentHotkey { get; set; } = new();
    
    public LayoutSwitcher LayoutSwitcher { get; set; }
    
    
    

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

    public void RemoveCurrentHotkey() => Hotkeys.Remove(CurrentHotkey);

    private void OnKeyReadingChanged(bool value)
    {
        if (value)
            KeyReader.Start();
        else
            KeyReader.Stop();
    }

    public async void Save()
    {
        ClipboardHotkeyManager.Clear();
        foreach (var hotkey in Hotkeys)
        {
            ClipboardHotkeyManager.AddHotkey_SetToBoard(hotkey.KeysToBoard.ToArray(), hotkey.BoardName);
            ClipboardHotkeyManager.AddHotkey_SetToClipBoard(hotkey.KeysToClipBoard.ToArray(), hotkey.BoardName);
        }

        await Task.Run(SaveToFile);
    }

    public void SaveToFile()
    {
        var converter = new ListDoubleHotkeyConverter();
        var ljson = converter.ToLjson(Hotkeys);
        File.WriteAllText(_filePath, ljson);
    }
    public void Read()
    {
        var ljson = File.ReadAllText(_filePath);
        var converter = new ListDoubleHotkeyConverter();
        var result = converter.FromLjson(ljson);
        Hotkeys.Clear();
        foreach (var hotkey in result)
        {
            Hotkeys.Add(hotkey);
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
    
    private void OnCurrentLayoutChanged(int index)
    {
        Hotkeys = LayoutSwitcher.CurrentLayout.Hotkeys;
        _filePath = LayoutSwitcher.CurrentLayout.FilePath;
    }
    
    private void OnFilePathChanged(string value)
    {
        _filePath = LayoutSwitcher.CurrentLayout.FilePath;
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