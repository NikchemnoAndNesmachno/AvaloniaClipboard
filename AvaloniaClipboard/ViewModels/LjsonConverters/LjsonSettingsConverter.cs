using System.Collections.Generic;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class LjsonSettingsConverter: LjsonDefaultConvert<SettingsViewModel>
{
    private LjsonLayoutConverter LayoutConverter { get; set; } = new();
    public LjsonSettingsConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = '٥'
        };
    }

    public override IList<object> ExtractValues(SettingsViewModel obj)
    {
        var list = new object[2 + obj.LayoutSwitcher.Layouts.Count];
        list[0] = obj.SelectedColorIndex;
        list[1] = obj.SelectedLanguageIndex;
        for (int i = 0; i < obj.LayoutSwitcher.Layouts.Count; i++)
        {
            var ljson = LayoutConverter.ToLjson(obj.LayoutSwitcher.Layouts[i]);
            list[i+2] = ljson;
        }
        return list;
    }

    public override void AssignValues(SettingsViewModel obj, IList<string> values)
    {
        obj.SelectedColorIndex = int.Parse(values[0]);
        obj.SelectedLanguageIndex = int.Parse(values[1]);
        obj.LayoutSwitcher.Layouts.Clear();
        for (int i = 2; i < values.Count; i++)
        {
            var layout = new OBoardLayout();
            LayoutConverter.FromLjson(layout, values[i]);
            obj.LayoutSwitcher.Layouts.Add(layout);
        }
    }
}