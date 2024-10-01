using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaClipboard.Models;
using AvaloniaClipboard.Models.Interfaces;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels.Observables;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public WatchViewModel WatchViewModel { get; set; }
    public HotkeyViewModel HotkeyViewModel { get; set; }
    
    public MainWindowViewModel(IClipboard clipboard)
    {
        var hotkeyManager = ServiceManager.Get<HotkeyManager>();
        HotkeyViewModel = new HotkeyViewModel(clipboard, hotkeyManager);
        WatchViewModel = new WatchViewModel(hotkeyManager);
    }
    
}