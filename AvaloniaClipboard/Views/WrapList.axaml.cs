using System.Collections;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace AvaloniaClipboard.Views;

public class WrapList : TemplatedControl
{
    public static readonly StyledProperty<IList> ItemsProperty = AvaloniaProperty.Register<WrapList, IList>(
        nameof(Items));
    
    public IList Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public void RemoveItem(object? item)
    {
        Items.Remove(item);
    }
}