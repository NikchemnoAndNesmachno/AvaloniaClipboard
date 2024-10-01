using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media;
using SharpHook.Native;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.Views;

public static class Converters
{
    public static FuncValueConverter<bool?, SolidColorBrush> ActivatedToColor =>
        new(value => value is { } state ? state ? 
            new SolidColorBrush(Colors.Lime) : 
            new SolidColorBrush(Colors.Red) :
            new SolidColorBrush(Colors.Pink));
}

