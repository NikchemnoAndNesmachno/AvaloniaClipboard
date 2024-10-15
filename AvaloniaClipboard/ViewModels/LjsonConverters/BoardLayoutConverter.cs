using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class BoardLayoutConverter: LjsonDefaultConvert<ObservableBoardLayout>
{
    private ListDoubleHotkeyConverter ListDoubleHotkeyConverter { get; set; }
    public BoardLayoutConverter()
    {
        ListDoubleHotkeyConverter = new();
        ConvertStrategy = new SimpleStrategy()
        {
            Char = '٦'
        };
    }
    public override IList<object> ExtractValues(ObservableBoardLayout obj) =>
    [
        obj.Name,
        obj.FilePath,
        ListDoubleHotkeyConverter.ToLjson(obj.Hotkeys)
    ];

    public override void AssignValues(ObservableBoardLayout obj, IList<string> values)
    {
        obj.Name = values[0];
        obj.FilePath = values[1];
        obj.Hotkeys = new(ListDoubleHotkeyConverter.FromLjson(values[2]));
    }
}