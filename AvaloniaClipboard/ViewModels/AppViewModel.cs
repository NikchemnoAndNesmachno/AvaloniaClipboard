using AvaloniaClipboard.Services;

namespace AvaloniaClipboard.ViewModels;

public class AppViewModel: ViewModelBase
{
    public void Open()
    {
        var windowManager = ServiceManager.Get<IWindowManager>();
        windowManager.OpenWindow();
    }

    public void Close()
    {
    }
}