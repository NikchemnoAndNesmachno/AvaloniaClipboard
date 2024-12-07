using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;
using SharpHook.Native;
using SharpHotHook.Interfaces;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class LjsonHotkeyConverter: LjsonDefaultConvert<IHotkey>
{
    public LjsonHotkeyConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = '|'
        };
    }
    public override IList<object> ExtractValues(IHotkey obj)
    {
        var array = new object[obj.KeyCodes.Count];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = (int)obj.KeyCodes[i];
        }

        return array;
    }

    public override void AssignValues(IHotkey obj, IList<string> values)
    {
        ObservableCollection<KeyCode> codes = []; 
        foreach (var value in values)
        {
            codes.Add((KeyCode)int.Parse(value));
        }

        obj.KeyCodes = codes;
    }
}