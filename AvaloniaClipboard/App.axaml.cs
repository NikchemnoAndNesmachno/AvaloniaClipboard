using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels;
using AvaloniaClipboard.Views;
using AvaloniaClipboard.Views.ServiceImplements;

namespace AvaloniaClipboard;

public partial class App : Application
{
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                desktopLifetime.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            }
            ServiceManager.RegisterSingleton<MainWindowViewModel>();
            ServiceManager.RegisterTransient<MainWindow>();
            ServiceManager.RegisterSingleton<IWindowManager, WindowManager<MainWindow>>();
            ServiceManager.Init();
            DataContext = new AppViewModel();
            HotKeyManager.SetHotKey(this, new KeyGesture(Key.S, KeyModifiers.Control));
            
        }
        
        base.OnFrameworkInitializationCompleted();
    }
    

}