using AvaloniaClipboard.Models.Interfaces;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels;
using AvaloniaClipboard.Views;
using AvaloniaClipboard.Views.ServiceImplements;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaClipboard;

public static class ServiceRegister
{
    public static void RegisterAll()
    {
        RegisterViewModels();
        RegisterWindows();
        RegisterServices();
        ServiceManager.Init();
    }

    public static void RegisterWindows()
    {
        ServiceManager.Services.AddSingleton<MainWindow>();
    }

    public static void RegisterViewModels()
    {
        ServiceManager.Services.AddSingleton<MainWindowViewModel>();
    }
    public static void RegisterServices()
    {
       
        ServiceManager.Services.AddSingleton<IWindowManager, WindowManager<MainWindow>>();
        ServiceManager.Services.AddSingleton<IExitApp, ExitApp>();
        ServiceManager.Services.AddSingleton<IClipboard, ClipboardService>(x=>
            new ClipboardService(ServiceManager.Get<MainWindow>()));
    }
    
}