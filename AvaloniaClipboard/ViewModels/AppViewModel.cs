using System;
using System.Collections.ObjectModel;
using AvaloniaClipboard.Models;
using AvaloniaClipboard.Models.Interfaces;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels.Observables;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels;

public class AppViewModel: ViewModelBase
{
    public ClipboardHotkeyManager ClipboardManager { get; set; } = new ClipboardHotkeyManager()
    {
        BoardManager = new BoardManager<ObservableBoard>(),
        Clipboard = ServiceManager.Get<IClipboard>(),
        HotkeyManager = new HotkeyManager()
        {
            HotkeyContainer = new ObservableHotkeyContainer(),
            PressedKeys = new ObservableCollection<KeyCode>()
        }
    };
    public AppViewModel()
    {
    }
   
    public void Open()
    {
        var windowManager = ServiceManager.Get<IWindowManager>();
        windowManager.OpenWindow();
    }
    public void Exit()
    {
        var exitApp = ServiceManager.Get<IExitApp>();
        exitApp.Exit();
        var vm = ServiceManager.Get<MainWindowViewModel>();
        vm.Dispose();
    }
    public void Close()
    {
    }
}