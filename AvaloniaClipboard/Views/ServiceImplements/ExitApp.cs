using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaClipboard.Services;

namespace AvaloniaClipboard.Views.ServiceImplements;

public class ExitApp: IExitApp
{
    public void Exit()
    {
        var app = Application.Current;
        if (app?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
}