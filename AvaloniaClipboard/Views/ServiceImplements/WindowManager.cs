using Avalonia.Controls;
using AvaloniaClipboard.Services;

namespace AvaloniaClipboard.Views.ServiceImplements;

public class WindowManager<T>(T window) : IWindowManager where T : Window
{
    private T? _window = window;

    private T Window
    {
        get
        {
            if (_window is not null) return ServiceManager.Get<T>();
            _window = ServiceManager.Get<T>();
            _window.Closed += (_, _) => _window = null;
            return _window;
        }
        set => _window = value;
    }

    public void OpenWindow()
    {
        Window.Show();
    }
}