using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels.Observables;

public class OKeyReader: KeyReaderManager, INotifyPropertyChanged
{
    private KeyCode _key = KeyCode.VcUndefined;
    
    public override KeyCode CurrentKey
    {
        get => _key;
        set => SetField(ref _key, value);
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }

    public OKeyReader()
    {
        PressedKeys = new ObservableCollection<KeyCode>();
    }
}