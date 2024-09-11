using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaClipboard.ViewModels.Observables;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels;

public class MainWindowViewModel : ViewModelBase, IDisposable
{
    private bool _isKeyReading = true;
    private KeyCode _key = KeyCode.VcUndefined;
    public ObservableDoubleHotkey DoubleHotkey { get; set; } = new();
    public ObservableCollection<ObservableDoubleHotkey> Hotkeys { get; set; } = new();
    
    public MainWindowViewModel()
    {
        this.WhenAnyValue(x => x.IsKeyReading).Subscribe(OnStopChanged);
        this.WhenAnyValue(x => x.KeyReader.KeyReadContainer.CurrentKey).Subscribe(x => CurrentKey = x);
        //this.WhenAnyValue(x => KeyReader.CurrentKey).Subscribe(code => CurrentKey = code);
    }

    public IList<KeyCode> PressedKeys
    {
        get => KeyReader.PressedKeys;
        set => KeyReader.PressedKeys = value;
    }

    public KeyCode CurrentKey
    {
        get => _key;
        set => this.RaiseAndSetIfChanged(ref _key, value);
    }

    public bool IsKeyReading
    {
        get => _isKeyReading;
        set => this.RaiseAndSetIfChanged(ref _isKeyReading, value);
    }

    public IList<KeyCode> Keys => KeyReader.PressedKeys;

    private KeyReaderManager KeyReader { get; } = new()
    {
        KeyReadContainer = new ObservableKeyContainer(),
        PressedKeys = new ObservableCollection<KeyCode>()
    };


    public void Dispose()
    {
        KeyReader.Stop();
        KeyReader.Dispose();
    }

    public void AddNewHotkey()
    {
        foreach (var hotkey in Hotkeys)
        {
            if(hotkey.BoardName == DoubleHotkey.BoardName) return;
        }
        Hotkeys.Add(DoubleHotkey);
    }
    public void UpdateHotkey()
    {
        
    }
    private void OnStopChanged(bool value)
    {
        if (value)
            KeyReader.Stop();
        else
            KeyReader.Run();
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