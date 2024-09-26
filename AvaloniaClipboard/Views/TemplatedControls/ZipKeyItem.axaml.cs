using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace AvaloniaClipboard.Views.TemplatedControls;

public class ZipKeyItem : TemplatedControl
{
    public static readonly StyledProperty<string> KeyCodeProperty =
        AvaloniaProperty.Register<ZipKeyItem, string>(nameof(KeyCode));

    public static readonly StyledProperty<bool> IsActivatedProperty =
        AvaloniaProperty.Register<ZipKeyItem, bool>(nameof(IsActivated));
    
    public bool IsActivated
    {
        get => GetValue(IsActivatedProperty);
        set => SetValue(IsActivatedProperty, value);
    }
    public string KeyCode
    {
        get => GetValue(KeyCodeProperty);
        set => SetValue(KeyCodeProperty, value);
    }
    

}