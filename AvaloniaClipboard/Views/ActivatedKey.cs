using System.Reflection.Emit;
using SharpHook.Native;

namespace AvaloniaClipboard.Views;

public class ActivatedKey(KeyCode code, bool active)
{
    public bool Active { get; set; } = active;
    public KeyCode KeyCode { get; set; } = code;
}