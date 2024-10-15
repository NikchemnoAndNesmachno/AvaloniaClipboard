using System.Collections.ObjectModel;
using ReactiveUI;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableBoardLayout: ObservableBase
{
    private string _name = "";
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string _filePath = "";

    public string FilePath
    {
        get => _filePath;
        set => this.RaiseAndSetIfChanged(ref _filePath, value);
    }

    public ObservableCollection<ObservableDoubleHotkey> Hotkeys { get; set; } = [];

    public static ObservableBoardLayout Default => new()
    {
        Name = "New",
        FilePath = "new_file.txt"
    };
}