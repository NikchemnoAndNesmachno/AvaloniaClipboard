using System.Threading.Tasks;
using Avalonia.Controls;
using AvaloniaClipboard.Services;

namespace AvaloniaClipboard.Views.ServiceImplements;

public class ClipboardService : IClipboard
{
    private MainWindow? _window;

    public MainWindow Window => _window ??= ServiceManager.Get<MainWindow>();

    public async Task<string> GetTextAsync()
    {
        var clip = Window.Clipboard;
        if (clip is null) return "";
        return await clip.GetTextAsync() ?? "";
    }

    public async Task SetTextAsync(string text)
    {
        var clip = Window.Clipboard;
        if (clip is null) return;
        await clip.SetTextAsync(text);
    }
}