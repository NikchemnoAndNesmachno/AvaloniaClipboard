using Avalonia.Styling;

namespace AvaloniaClipboard.Styles.Resources.ColorThemes;

public class CustomThemes
{
    public static ThemeVariant Pink { get; set; } = new ThemeVariant("PinkTheme", ThemeVariant.Light);
    public static ThemeVariant Black { get; set; } = new ThemeVariant("DarkTheme", ThemeVariant.Dark);
}