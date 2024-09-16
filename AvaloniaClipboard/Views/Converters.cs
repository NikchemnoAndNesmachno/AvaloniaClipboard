using Avalonia.Data.Converters;

namespace AvaloniaClipboard.Views;

public static class Converters
{
    public static FuncValueConverter<bool?, string> StartToggleConverter =>
        new(value => value is { } state ? state ? "Зупинити" : "Розпочати" : "");
}