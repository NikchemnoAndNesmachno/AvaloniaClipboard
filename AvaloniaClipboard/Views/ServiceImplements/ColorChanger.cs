using System.Collections.Generic;
using Avalonia;
using Avalonia.Styling;
using AvaloniaClipboard.Resources.ColorThemes;
using AvaloniaClipboard.Services;

namespace AvaloniaClipboard.Views.ServiceImplements;

public class ColorChanger: IColorChanger
{
    private IList<ThemeVariant> Variants { get; set; } =
        [
            CustomThemes.Black, CustomThemes.Pink
        ];
    public void ChangeColorScheme(int colorIndex)
    {
        var current = Application.Current;
        if (current != null) current.RequestedThemeVariant = Variants[colorIndex];
    }
}