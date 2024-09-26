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
    public static FuncValueConverter<bool?, string> StartToggleConverter =>
        new(value => value is { } state ? state ? "Зупинити" : "Розпочати" : "");
    
    public static FuncValueConverter<bool?, SolidColorBrush> ActivatedToColor =>
        new(value => value is { } state ? state ? 
            new SolidColorBrush(Colors.Lime) : 
            new SolidColorBrush(Colors.Orange) :
            new SolidColorBrush(Colors.Pink)
            );

    public static FuncValueConverter<IList, IList> ListToIndexes =>
        new(value =>
        {
            if (value is null) return new List<object>();
            return Enumerable.Range(0, value.Count).ToList();
        });

    public static FuncValueConverter<IList<KeyCode>, string> FirstKey =>
        new(value => value != null ? value[0].ToString() : KeyCode.VcUndefined.ToString()) ;

    public static FuncValueConverter<IList<bool>, SolidColorBrush> FirstActivated =>
        new(value => value is null ? 
            new SolidColorBrush(Colors.BlueViolet) :
            value[0] ? 
                new SolidColorBrush(Colors.Lime) : 
                new SolidColorBrush(Colors.Orange));
}

