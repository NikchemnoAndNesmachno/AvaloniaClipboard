using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using AvaloniaClipboard.Services;

namespace AvaloniaClipboard.Views.ServiceImplements;

public class FileService(MainWindow window): IFileService
{
    public async Task<string> BrowseSaveFile()
    {
        var file = await window.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
        {
            Title = "Save file"
        });
        return file is null ? 
            "" :
            Uri.UnescapeDataString(file.Path.AbsolutePath);
    }

    public async Task<string> BrowseOpenFile()
    {
        var files = await window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open file",
            AllowMultiple = false
        });
        return files.Count == 0 ? 
            "" :
            Uri.UnescapeDataString(files[0].Path.AbsolutePath);
    }
}