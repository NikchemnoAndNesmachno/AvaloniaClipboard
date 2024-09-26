using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels.Observables;
using DynamicData;
using SharpHook.Native;
using SharpHotHook;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels;

public class WatchViewModel(HotkeyManager hotkeyManager): ViewModelBase
{
    public HotkeyManager HotkeyManager { get; set; } = hotkeyManager;
    public ObservableCollection<ObservableCollection<ObservableZipItem>> Zips { get; set; } = [];
    private IList<IHotkey> Hotkeys => HotkeyManager.Hotkeys;
    public void Refresh()
    {
        Clear();
        foreach (var hotkey in Hotkeys)
        {
            var n = new ObservableCollection<ObservableZipItem>();
            for (int i = 0; i < hotkey.IsActivated.Count; i++)
            {
                if(hotkey.IsActivated is not ObservableCollection<bool> bools) continue;
                var zip = new ObservableZipItem();
                zip.Register(bools, hotkey.KeyCodes, i);
                n.Add(zip);
            }
            Zips.Add(n);
        }
    }

    public void Clear()
    {
        for (int index = 0; index < Zips.Count; index++)
        {
            var hotkey = Hotkeys[index];
            if(hotkey.IsActivated is not ObservableCollection<bool> bools) continue;
            var zipItems = Zips[index];
            foreach (var zip in zipItems)
            {
                zip.Unregister(bools);
            }
        }
        Zips.Clear();
    }
}