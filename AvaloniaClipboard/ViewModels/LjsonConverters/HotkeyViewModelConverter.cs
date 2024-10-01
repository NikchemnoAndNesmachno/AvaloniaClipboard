using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;
namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class HotkeyViewModelConverter: LjsonListConvert<ObservableDoubleHotkey>
{
    private ObservableHotkeyConverter HotkeyConverter { get; set; } = new();
    public HotkeyViewModelConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = 'µ'
        };
    }
    public override IList<object> ExtractValues(IList<ObservableDoubleHotkey> obj)
    {
        IList<object> l = new object[obj.Count];
        for (int i = 0; i < obj.Count; i++)
        {
            l[i] = HotkeyConverter.ToLjson(obj[i]);
        }
        return l;
    }

    public override void AssignValues(IList<ObservableDoubleHotkey> obj, IList<string> values)
    {
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i] = HotkeyConverter.FromLjson(values[i]);
        }
    }
}