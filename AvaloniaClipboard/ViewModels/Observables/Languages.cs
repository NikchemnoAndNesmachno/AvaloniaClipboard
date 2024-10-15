namespace AvaloniaClipboard.ViewModels.Observables;

public class Languages(string code, string name)
{
    public string CultureCode { get; set; } = code;
    public string CultureName { get; set; } = name;
}