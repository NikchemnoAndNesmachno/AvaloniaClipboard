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

public class MainWindowViewModel(HotkeyViewModel hotkeyViewModel, WatchViewModel watchViewModel, SettingsViewModel settingsViewModel)
    : ViewModelBase
{
    public WatchViewModel WatchViewModel { get; set; } = watchViewModel;
    public HotkeyViewModel HotkeyViewModel { get; set; } = hotkeyViewModel;
    public SettingsViewModel SettingsViewModel { get; set; } = settingsViewModel;
}