using System.Collections.Generic;
using AvaloniaClipboard.Models.Defaults;
using AvaloniaClipboard.Models.Interfaces;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.Models;

public interface IClipboardHotkeyManager
{
    HotkeyManager HotkeyManager { get; set; }

    IBoardManager BoardManager { get; set; }
    void SetTextToClipboard(string boardName);

    void SetTextFromClipBoard(string boardName);

    void RemoveHotkey(KeyCode[] keys, string boardName); 
    void SetHotkey(string boardName, KeyCode[] keys, int index);

    void AddHotkey_SetToBoard(KeyCode[] keys, string boardName);


    void AddHotkey_SetToClipBoard(KeyCode[] keys, string boardName);

    void Clear();
}