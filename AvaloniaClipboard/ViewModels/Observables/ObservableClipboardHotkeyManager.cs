using System.Collections.ObjectModel;
using AvaloniaClipboard.Models;
using AvaloniaClipboard.Models.Interfaces;
using AvaloniaClipboard.Services;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableClipboardHotkeyManager: ClipboardHotkeyManager
{
    public ObservableClipboardHotkeyManager(IClipboard clipboard) : base(clipboard)
    {
        BoardManager = new BoardManager<ObservableBoard>();
        HotkeyManager = new HotkeyManager
        {
            HotkeyContainer = new ObservableHotkeyContainer(),
            PressedKeys = new ObservableCollection<KeyCode>()
        };
    }
}