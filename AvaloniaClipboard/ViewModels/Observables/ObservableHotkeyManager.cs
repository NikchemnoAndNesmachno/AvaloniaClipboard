using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharpHotHook;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableHotkeyManager: HotkeyManager
{
    public override IList<IHotkey> Hotkeys { get; set; } = new ObservableCollection<IHotkey>();
}