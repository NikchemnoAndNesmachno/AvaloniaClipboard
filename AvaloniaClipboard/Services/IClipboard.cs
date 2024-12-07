using System.Threading.Tasks;

namespace AvaloniaClipboard.Services;

public interface IClipboard
{
    public Task<string> GetTextAsync();
    public Task SetTextAsync(string text);
}