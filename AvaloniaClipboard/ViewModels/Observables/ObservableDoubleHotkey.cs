using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using SharpHook.Native;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableDoubleHotkey: ObservableBase
{
    private string _boardName = "";

    public string BoardName
    {
        get => _boardName;
        set => this.RaiseAndSetIfChanged(ref _boardName, value);
    }

    public ObservableCollection<KeyCode> KeysToBoard { get; set; } = [];
    public ObservableCollection<KeyCode> KeysToClipBoard { get; set; } = [];
}