using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableKeyContainer : ObservableBase, IKeyReadContainer
{
    private KeyCode _currentKey;

    public KeyCode CurrentKey
    {
        get => _currentKey;
        set => this.RaiseAndSetIfChanged(ref _currentKey, value);
    }
}