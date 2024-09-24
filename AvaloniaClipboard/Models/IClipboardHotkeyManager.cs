using System.Collections.Generic;
using Avalonia.Input;
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

    void RemoveHotkey(IList<KeyCode> keys, string boardName); 
    void SetHotkey(string boardName, IList<KeyCode> keys, int index);

    void AddHotkey_SetToBoard(IList<KeyCode> keys, string boardName);


    void AddHotkey_SetToClipBoard(IList<KeyCode> keys, string boardName);

    void Clear();
}