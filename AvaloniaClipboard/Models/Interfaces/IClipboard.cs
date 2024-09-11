using System.Threading.Tasks;

namespace AvaloniaClipboard.Models.Interfaces;

public interface IClipboard
{
    public Task<string> GetTextAsync();
    public Task SetTextAsync(string text);
}