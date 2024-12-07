using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaClipboard.Resources.ColorThemes;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels;
using AvaloniaClipboard.Views;
using HotHook = SharpHotHook;

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
            desktop.MainWindow = ServiceManager.Get<MainWindow>();
        }
        base.OnFrameworkInitializationCompleted();

    }
    
    private void TrayIcon_OnClicked(object? sender, EventArgs e)
    {
        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
        var window = desktop.MainWindow;
        if (window is null) return;
        if (window.WindowState == WindowState.Minimized)
        {
            window.WindowState = WindowState.Normal;
        }
        else if (window.IsVisible)
        {
            window.Close();
        }
        else
        {
            window.WindowState = WindowState.Normal;
            window.Show();
        }
        
    }

    private void OnExit(object? sender, EventArgs e)
    {
        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
        var manager = ServiceManager.Get<HotHook.HotkeyManager>();
        manager.Stop();
        desktop.Shutdown();
    }
}