using System.Collections.ObjectModel;
using AvaloniaClipboard.Models;
using AvaloniaClipboard.Models.Interfaces;
using AvaloniaClipboard.Services;
using SharpHook.Native;
using SharpHotHook;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableClipboardHotkeyManager: ClipboardHotkeyManager<ObservableHotkey>
{
    public ObservableClipboardHotkeyManager(IClipboard clipboard) : base(clipboard)
    {
        BoardManager = new BoardManager<ObservableBoard>();
        HotkeyManager = new HotkeyManager
        {
            Hotkeys = new ObservableCollection<IHotkey>(),
            PressedKeys = new ObservableCollection<KeyCode>()
        };
    }
}