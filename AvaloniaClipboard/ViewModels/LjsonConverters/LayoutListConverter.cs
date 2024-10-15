using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class LayoutListConverter: LjsonListConvert<ObservableBoardLayout>
{
    private NoKeysBoardLayoutConverter BoardLayoutConverter { get; set; } = new();
    public LayoutListConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = '\u3333'
        };
    }

    public override IList<object> ExtractValues(IList<ObservableBoardLayout> obj)
    {
        IList<object> l = new object[obj.Count];
        for (int i = 0; i < obj.Count; i++)
        {
            l[i] = BoardLayoutConverter.ToLjson(obj[i]);
        }
        return l;
    }

    public override void AssignValues(IList<ObservableBoardLayout> obj, IList<string> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            obj[i] = BoardLayoutConverter.FromLjson(values[i]);
        }
    }
}