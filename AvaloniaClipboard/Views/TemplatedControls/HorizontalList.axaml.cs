using System.Collections;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace AvaloniaClipboard.Views.TemplatedControls;

public class HorizontalList : TemplatedControl
{
    public static readonly StyledProperty<IList> ItemSourceProperty =
        AvaloniaProperty.Register<HorizontalList, IList>(nameof(ItemSource));

    public IList ItemSource
    {
        get => GetValue(ItemSourceProperty);
        set => SetValue(ItemSourceProperty, value);
    }
}