using ReactiveUI;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels.Observables;

public class OBoard : ReactiveObject
{
    public OBoard()
    {
        ToClipboardHotkey = new OHotkey();
        FromClipboardHotkey = new OHotkey();
    }
    private string
        _name = "",
        _data = "";

    private IHotkey 
        _toClipboardHotkey = new OHotkey(),
        _fromClipboardHotkey = new OHotkey();

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

    public IHotkey ToClipboardHotkey
    {
        get => _toClipboardHotkey;
        set => this.RaiseAndSetIfChanged(ref _toClipboardHotkey, value);
    }

    public IHotkey FromClipboardHotkey
    {
        get => _fromClipboardHotkey;
        set => this.RaiseAndSetIfChanged(ref _fromClipboardHotkey, value);
    }
    
}