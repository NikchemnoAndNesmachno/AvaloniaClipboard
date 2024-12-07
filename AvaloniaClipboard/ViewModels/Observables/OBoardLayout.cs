using ReactiveUI;

namespace AvaloniaClipboard.ViewModels.Observables;

public class OBoardLayout: ReactiveObject
{
    private string _name = "", _filePath="";
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string FilePath
    {
        get => _filePath;
        set => this.RaiseAndSetIfChanged(ref _filePath, value);
    }

    public static OBoardLayout Default => new()
    {
        Name = "New",
        FilePath = "new_file.txt"
    };
}