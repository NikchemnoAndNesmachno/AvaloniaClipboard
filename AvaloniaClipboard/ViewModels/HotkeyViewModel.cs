using System;
using System.Collections.ObjectModel;
using System.IO;
using AvaloniaClipboard.ViewModels.LjsonConverters;
using AvaloniaClipboard.ViewModels.Observables;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels;

public class HotkeyViewModel: ReactiveObject, IDisposable
{
    public HotkeyManager HotkeyRunner { get; set; } = new()
    {
        PressedKeys = new ObservableCollection<KeyCode>(),
        Hotkeys = new ObservableCollection<IHotkey>()
    };
    public HotkeyViewModel(BoardManager boardManager, LayoutSwitcher layoutSwitcher)
    {
        BoardManager = boardManager;
        LayoutSwitcher = layoutSwitcher;
        this.WhenAnyValue(x => x.IsKeyReading).Subscribe(OnKeyReadingChanged);
        this.WhenAnyValue(x => x.IsStarted).Subscribe(OnStarted);
        this.WhenAnyValue(x => x.KeyReader.CurrentKey).Subscribe(x=>CurrentKey =x);
        layoutSwitcher.WhenAnyValue(x => x.CurrentLayoutIndex).Subscribe(OnCurrentLayoutChanged);
        layoutSwitcher.WhenAnyValue(x => x.CurrentLayout.FilePath).Subscribe(OnFilePathChanged);
        NewHotkeyBoard = new OBoard()
        {
            Name = "TestHotkeyBoard"
        };
        this.WhenAnyValue(x => x.CurrentHotkeyBoard).Subscribe(OnCurrentBoardChanged);
    }
    public BoardManager BoardManager { get; set; }
    private bool _isKeyReading, _isStarted;
    private string _filePath = "";
    private KeyCode _keyCode = KeyCode.VcUndefined;
    private OKeyReader KeyReader { get; } = new();
    public OBoard? CurrentHotkeyBoard { get; set; }

    public OBoard NewHotkeyBoard { get; set; }
    
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
        HotkeyRunner.Stop();
        HotkeyRunner.Dispose();
    }

    public void AddNewHotkey()
    {
        BoardManager.AddHotkeyBoardCopy(NewHotkeyBoard);
    }
    public void ClearNewHotkey()
    {
        NewHotkeyBoard.Name = "";
        NewHotkeyBoard.FromClipboardHotkey.KeyCodes.Clear();
        NewHotkeyBoard.ToClipboardHotkey.KeyCodes.Clear();
    }

    public void RemoveCurrentBoard()
    {
        if(CurrentHotkeyBoard is null) return;
        BoardManager.Remove(CurrentHotkeyBoard);
    }

    private void OnKeyReadingChanged(bool value)
    {
        if (value)
            KeyReader.Start();
        else
            KeyReader.Stop();
    }
    
    private void OnCurrentBoardChanged(OBoard? value)
    {
        if(value is null ) return;
        NewHotkeyBoard.Name = value.Name;
        NewHotkeyBoard.ToClipboardHotkey.KeyCodes 
            = new ObservableCollection<KeyCode>(value.ToClipboardHotkey.KeyCodes);
        NewHotkeyBoard.FromClipboardHotkey.KeyCodes 
            = new ObservableCollection<KeyCode>(value.FromClipboardHotkey.KeyCodes);
    }

    public void Apply()
    {
        HotkeyRunner.Hotkeys.Clear();
        foreach (var board in BoardManager.Boards)
        {
            HotkeyRunner.Add(board.FromClipboardHotkey);
            HotkeyRunner.Add(board.ToClipboardHotkey);
        }
    }

    public void SaveToFile()
    {
        var converter = new LjsonBoardManagerConverter();
        var ljson = converter.ToLjson(BoardManager);
        File.WriteAllText(_filePath, ljson);
    }
    public void Read()
    {
        try
        {
            var ljson = File.ReadAllText(_filePath);
            var converter = new LjsonBoardManagerConverter();
            converter.FromLjson(BoardManager, ljson);
            Apply();
        }
        catch (Exception)
        {
            // ignored
        }
        
        
    }
    private void OnStarted(bool value)
    {
        if (value)
        {
            IsKeyReading = false;
            HotkeyRunner.Start();
        }
        else
        {
            HotkeyRunner.Stop();
        }
    }
    
    private void OnCurrentLayoutChanged(int index) => 
        _filePath = LayoutSwitcher.CurrentLayout.FilePath;

    private void OnFilePathChanged(string value) => 
        _filePath = LayoutSwitcher.CurrentLayout.FilePath;

    public void NewKeyFromClipBoard()
    {
        if(CurrentKey == KeyCode.VcUndefined) return;
        NewHotkeyBoard.FromClipboardHotkey.KeyCodes.Add(CurrentKey);
    }

    public void NewKeyToClipBoard()
    {
        if(CurrentKey == KeyCode.VcUndefined) return;
        NewHotkeyBoard.ToClipboardHotkey.KeyCodes.Add(CurrentKey);
    }
}