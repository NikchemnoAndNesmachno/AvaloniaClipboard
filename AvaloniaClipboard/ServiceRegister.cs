using System.Collections.ObjectModel;
using AvaloniaClipboard.Models;
using AvaloniaClipboard.Models.Interfaces;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels;
using AvaloniaClipboard.ViewModels.Observables;
using AvaloniaClipboard.Views;
using AvaloniaClipboard.Views.ServiceImplements;
using Microsoft.Extensions.DependencyInjection;
using SharpHook.Native;
using SharpHotHook;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard;

public static class ServiceRegister
{
    public static void RegisterAll()
    {
        RegisterServices();
        ServiceManager.Init();
    }

    public static void RegisterServices()
    {
        ServiceManager.Services.AddSingleton<IClipboard, ClipboardService>();
        ServiceManager.Services.AddSingleton<IBoardManager, BoardManager<ObservableBoard>>();
        ServiceManager.Services.AddSingleton(new HotkeyManager
        {
            Hotkeys = new ObservableCollection<IHotkey>(),
            PressedKeys = new ObservableCollection<KeyCode>()
        });
    }
}