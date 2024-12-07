using System.Collections.ObjectModel;
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
        ServiceManager.Services.AddSingleton<ILanguageChanger, LanguageChanger>();
        ServiceManager.Services.AddSingleton<IColorChanger, ColorChanger>();
        ServiceManager.Services.AddSingleton<LayoutSwitcher>();
        ServiceManager.Services.AddSingleton<MainWindowViewModel>();
        ServiceManager.Services.AddSingleton<SettingsViewModel>();
        ServiceManager.Services.AddSingleton<MainWindow>();
        ServiceManager.Services.AddSingleton<IClipboard, ClipboardService>();
        ServiceManager.Services.AddSingleton<BoardManager>();
        ServiceManager.Services.AddSingleton(new HotkeyManager
        {
            Hotkeys = new ObservableCollection<IHotkey>(),
            PressedKeys = new ObservableCollection<KeyCode>()
        });
        
        ServiceManager.Services.AddSingleton<HotkeyViewModel>();

        ServiceManager.Services.AddSingleton<IFileService, FileService>();
    }
}