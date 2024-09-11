using AvaloniaClipboard.Models.Interfaces;
using ReactiveUI;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableBoard : ObservableBase, ISimpleBoard
{
    private string
        _name = "",
        _data = "";

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string Data
    {
        get => _data;
        set => this.RaiseAndSetIfChanged(ref _data, value);
    }
}