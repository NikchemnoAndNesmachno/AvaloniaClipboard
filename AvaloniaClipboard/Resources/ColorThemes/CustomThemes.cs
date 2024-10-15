using Avalonia.Styling;

namespace AvaloniaClipboard.Resources.ColorThemes;

public static class CustomThemes
{
    public static ThemeVariant Pink { get; set; } = new("PinkTheme", ThemeVariant.Light);
    public static ThemeVariant Black { get; set; } = new("DarkTheme", ThemeVariant.Dark);
}