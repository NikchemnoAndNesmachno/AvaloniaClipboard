using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaClipboard.ViewModels.Observables;
using ReactiveUI;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels;

public class MainWindowViewModel : ViewModelBase, IDisposable
{
    private bool _isStopped = true;

    public MainWindowViewModel()
    {
        this.WhenAnyValue(x => x.IsStopped).Subscribe(OnStopChanged);
    }

    public KeyCode CurrentKey
    {
        get => KeyReader.CurrentKey;
        set => KeyReader.CurrentKey = value;
    }

    public bool IsStopped
    {
        get => _isStopped;
        set => this.RaiseAndSetIfChanged(ref _isStopped, value);
    }

    public IList<KeyCode> Keys => KeyReader.PressedKeys;

    private KeyReaderManager KeyReader { get; } = new()
    {
        KeyReadContainer = new ObservableKeyContainer(),
        PressedKeys = new ObservableCollection<KeyCode>()
    };


    public void Dispose()
    {
        KeyReader.Stop();
        KeyReader.Dispose();
    }

    private void OnStopChanged(bool value)
    {
        if (value)
            KeyReader.Stop();
        else
            KeyReader.Run();
    }
}