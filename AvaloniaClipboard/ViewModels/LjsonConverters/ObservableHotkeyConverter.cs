using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;
using SharpHook.Native;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class ObservableHotkeyConverter: LjsonDefaultConvert<ObservableDoubleHotkey>
{
    private KeyCodeListConverter KeysConverter { get; set; } = new();
    public ObservableHotkeyConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = '\u00b1'
        };
    }
    public override IList<object> ExtractValues(ObservableDoubleHotkey obj) =>
    [
        obj.BoardName, 
        KeysConverter.ToLjson(obj.KeysToBoard), 
        KeysConverter.ToLjson(obj.KeysToClipBoard)
    ];

    public override void AssignValues(ObservableDoubleHotkey obj, IList<string> values)
    {
        obj.BoardName = values[0];
        obj.KeysToBoard = new ObservableCollection<KeyCode>(KeysConverter.FromLjson(values[1]));
        obj.KeysToClipBoard = new ObservableCollection<KeyCode>(KeysConverter.FromLjson(values[2]));
    }
}