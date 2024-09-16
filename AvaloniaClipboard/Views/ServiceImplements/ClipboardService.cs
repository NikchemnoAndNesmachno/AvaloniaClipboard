using System.Threading.Tasks;
using Avalonia.Controls;
using AvaloniaClipboard.Models.Interfaces;

namespace AvaloniaClipboard.Views.ServiceImplements;

public class ClipboardService(MainWindow window) : IClipboard
{
    public async Task<string> GetTextAsync()
    {
        var clip = window.Clipboard;
        if (clip is null) return "";
        return await clip.GetTextAsync() ?? "";
    }

    public async Task SetTextAsync(string text)
    {
        var clip = window.Clipboard;
        if (clip is null) return;
        await clip.SetTextAsync(text);
    }
}