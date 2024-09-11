using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels;
using AvaloniaClipboard.Views;
using HotHook = SharpHotHook;
using HotHookCodes = SharpHook.Native.KeyCode;

namespace AvaloniaClipboard;

public class App : Application
{

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            ServiceRegister.RegisterAll();
            DataContext = new AppViewModel();
            desktop.MainWindow = ServiceManager.Get<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}