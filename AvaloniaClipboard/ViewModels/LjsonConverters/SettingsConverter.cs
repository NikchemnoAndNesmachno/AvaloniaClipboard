using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class SettingsConverter: SimpleStrategy
{
    private LayoutListConverter LayoutConverter { get; set; }= new();
    public SettingsConverter()
    {
        Char = '㑖';
    }

    public void AssignValues(SettingsViewModel vm, IList<string> values)
    {
        vm.SelectedColorIndex = int.Parse(values[0]);
        vm.SelectedLanguageIndex = int.Parse(values[1]);
        vm.LayoutSwitcher.Layouts = new ObservableCollection<ObservableBoardLayout>(
            LayoutConverter.FromLjson(values[2]));
        vm.LayoutSwitcher.CurrentLayoutIndex = int.Parse(values[3]);
    }

    public IList<object> ExtractValues(SettingsViewModel vm) =>
    [
        vm.SelectedColorIndex,
        vm.SelectedLanguageIndex,
        LayoutConverter.ToLjson(vm.LayoutSwitcher.Layouts),
        vm.LayoutSwitcher.CurrentLayoutIndex
    ];
}