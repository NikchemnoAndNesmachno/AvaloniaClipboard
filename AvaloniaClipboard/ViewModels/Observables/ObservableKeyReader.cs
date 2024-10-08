using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SharpHook.Native;
using SharpHotHook;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableKeyReader: KeyReaderManager, INotifyPropertyChanged
{
    private KeyCode _key = KeyCode.VcUndefined;
    
    public override KeyCode CurrentKey
    {
        get => _key;
        set => SetField(ref _key, value);
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}