using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;
using SharpHook.Native;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class KeyCodeListConverter: LjsonListConvert<KeyCode>
{
    public KeyCodeListConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = '.'
        };
    }

    public override IList<object> ExtractValues(IList<KeyCode> obj)
    {
        var array = new object[obj.Count];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = (int)obj[i];
        }
        return array;
    }

    public override void AssignValues(IList<KeyCode> obj, IList<string> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            obj[i]=(KeyCode)int.Parse(values[i]);
        }
    }
}