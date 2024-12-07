using System.Collections.Generic;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class LjsonBoardConverter: LjsonDefaultConvert<OBoard>
{
    private LjsonHotkeyConverter HotkeyConverter { get; set; } = new();
    public LjsonBoardConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = '睸'
        };
    }
    public override IList<object> ExtractValues(OBoard obj) =>
    [
        obj.Name,
        obj.Data,
        HotkeyConverter.ToLjson(obj.ToClipboardHotkey),
        HotkeyConverter.ToLjson(obj.FromClipboardHotkey)
    ];

    public override void AssignValues(OBoard obj, IList<string> values)
    {
        obj.Name = values[0];
        obj.Data = values[1];
        HotkeyConverter.FromLjson(obj.ToClipboardHotkey, values[2]);
        HotkeyConverter.FromLjson(obj.FromClipboardHotkey, values[3]);
    }
}