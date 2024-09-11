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
    private HotHook.HotkeyManager HotkeyManager { get; } = new();

    private void Register()
    {
        HotkeyManager.AddHotkey(
            [HotHookCodes.VcLeftControl, HotHookCodes.Vc1],
            () =>
            {
                Dispatcher.UIThread.Invoke(() =>
                {
                    var window = ServiceManager.Get<MainWindow>();
                    window.Keys.Text = "Щось та написано";
                    Console.WriteLine("Натичнуто");
                });
            });
    }

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