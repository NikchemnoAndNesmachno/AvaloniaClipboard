using AvaloniaClipboard.Models.Interfaces;

namespace AvaloniaClipboard.Models.Defaults;

public class DefaultBoard : ISimpleBoard
{
    public string Name { get; set; } = "";
    public string Data { get; set; } = "";
}