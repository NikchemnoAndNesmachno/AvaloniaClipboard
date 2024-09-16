using System.Collections.Generic;
using AvaloniaClipboard.Models.Defaults;
using AvaloniaClipboard.Models.Interfaces;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.Models;

public class ClipboardHotkeyManager(IClipboard clipboard): IClipboardHotkeyManager
{
    public Dictionary<string, KeyCode[][]> BoardHotkeys { get; set; } = [];
    public IClipboard? Clipboard { get; set; } = clipboard;
    public HotkeyManager HotkeyManager { get; set; } = new();

    public IBoardManager BoardManager { get; set; } = new BoardManager<DefaultBoard>();

    public async void SetTextToClipboard(string boardName)
    {
        if (Clipboard is null) return;
        var index = BoardManager.IndexOf(boardName);
        if (index == -1) return;
        await Clipboard.SetTextAsync(BoardManager.Get(index).Data);
    }

    public async void SetTextFromClipBoard(string boardName)
    {
        if (Clipboard is null) return;
        var index = BoardManager.IndexOf(boardName);
        if (index == -1) return;
        var board = BoardManager.Get(index);
        board.Data = await Clipboard.GetTextAsync();
    }

    public void RemoveHotkey(KeyCode[] keys, string boardName)
    {
        HotkeyManager.HotkeyContainer.Remove(keys);
        var boardIndex = BoardManager.IndexOf(boardName);
        BoardManager.Remove(BoardManager.Get(boardIndex));
    }
    public void SetHotkey(string boardName, KeyCode[] keys, int index)
    {
        var exists = BoardHotkeys.TryGetValue(boardName, out var hotkeys);
        if (!exists || hotkeys is null)
        {
            var newHotkeys = new KeyCode[2][];
            newHotkeys[index] = keys;
            BoardHotkeys.Add(boardName, newHotkeys);
        }
        else
        {
            hotkeys[index] = keys;
        }
    }
    public void AddHotkey_SetToBoard(KeyCode[] keys, string boardName)
    {
        var boardIndex = BoardManager.IndexOf(boardName);
        if (boardIndex == -1) BoardManager.Create(boardName);
        HotkeyManager.AddHotkey(keys, () => { SetTextFromClipBoard(boardName); });
        SetHotkey(boardName, keys, 1);
    }

   
    public void AddHotkey_SetToClipBoard(KeyCode[] keys, string boardName)
    {
        var boardIndex = BoardManager.IndexOf(boardName);
        if (boardIndex == -1) BoardManager.Create(boardName);
        HotkeyManager.AddHotkey(keys, () => { SetTextToClipboard(boardName); });
        SetHotkey(boardName, keys, 0);
    }

    public void Clear()
    {
        foreach (var pair in BoardHotkeys)
        {
            var boardIndex = BoardManager.IndexOf(pair.Key);
            if (boardIndex != -1)
            {
                BoardManager.Remove(BoardManager.Get(boardIndex));
            }

            foreach (var hotkey in pair.Value)
            {
                HotkeyManager.HotkeyContainer.Remove(hotkey);
            }
        }
        BoardHotkeys.Clear();
    }
}