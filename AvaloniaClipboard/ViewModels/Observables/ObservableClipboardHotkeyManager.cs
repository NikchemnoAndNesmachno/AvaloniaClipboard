using System.Collections.ObjectModel;
using Avalonia.Controls;
using AvaloniaClipboard.Models;
using AvaloniaClipboard.Models.Interfaces;
using AvaloniaClipboard.Services;
using SharpHook.Native;
using SharpHotHook;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableClipboardHotkeyManager: ClipboardHotkeyManager<ObservableHotkey>
{
    public ObservableClipboardHotkeyManager(IClipboard clipboard,
        IBoardManager boardManager,
        HotkeyManager hotkeyManager) : base(clipboard)
    {
        HotkeyManager = hotkeyManager;
        BoardManager = boardManager;
    }
}