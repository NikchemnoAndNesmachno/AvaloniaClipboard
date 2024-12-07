using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media;
using AvaloniaClipboard.ViewModels.Observables;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.Views;

public static class Converters
{
    public static readonly FuncValueConverter<bool?, SolidColorBrush> ActivatedToColorConverter =
        new(value => value is { } state ? state ? 
            new SolidColorBrush(Colors.Lime) : 
            new SolidColorBrush(Colors.Red) :
            new SolidColorBrush(Colors.Pink));
    
    public static readonly FuncValueConverter<IHotkey, ZipItems> BoardConverter =
        new(x => new ZipItems(x));

    public static FuncMultiValueConverter<bool, SolidColorBrush> IsAllActiveConverter { get; } =
        new(bools =>
        {
            var boolArray = bools.ToArray();
            if (boolArray[0]) return new SolidColorBrush(Colors.Lime);
            if (boolArray[1]) return new SolidColorBrush(Colors.DodgerBlue);
            return new SolidColorBrush(Colors.Black);
        });


}

