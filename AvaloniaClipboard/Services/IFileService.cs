using System.Threading.Tasks;

namespace AvaloniaClipboard.Services;

public interface IFileService
{ 
    Task<string> BrowseSaveFile();
    Task<string> BrowseOpenFile();
}