using System.Collections;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace AvaloniaClipboard.Views;

public class NewHotkeyView : TemplatedControl
{
    public static readonly StyledProperty<string> BoardNameProperty = AvaloniaProperty.Register<NewHotkeyView, string>(
        nameof(BoardName));

    public static readonly StyledProperty<IList> KeysToBoardProperty = AvaloniaProperty.Register<NewHotkeyView, IList>(
        nameof(KeysToBoard));

    public static readonly StyledProperty<IList> KeysToClipboardProperty = AvaloniaProperty.Register<NewHotkeyView, IList>(
        nameof(KeysToClipboard));

    public IList KeysToClipboard
    {
        get => GetValue(KeysToClipboardProperty);
        set => SetValue(KeysToClipboardProperty, value);
    }
    public IList KeysToBoard
    {
        get => GetValue(KeysToBoardProperty);
        set => SetValue(KeysToBoardProperty, value);
    }
    public string BoardName
    {
        get => GetValue(BoardNameProperty);
        set => SetValue(BoardNameProperty, value);
    }
}