using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using AvaloniaClipboard.Services;

namespace AvaloniaClipboard.Views.ServiceImplements;

public class LanguageChanger: ILanguageChanger
{
    private IList<string> Values { get; set; } =
    [
        "en", "ua"
    ];
    
    private string Process(string value) => 
        $"{Context}/{value}.axaml";

    private string Context => $@"avares://AvaloniaClipboard/Resources/Languages";
    public void ChangeLanguage(int langIndex)
    {
        var uri = new Uri(Process(Values[langIndex]));
        BaseDictionary[0] =new ResourceInclude((Uri?)null)
        {
            Source = uri
        }; 
    }
    
    private IList<IResourceProvider> BaseDictionary => Resources.MergedDictionaries;
    private IResourceDictionary Resources => Application.Current.Resources;
}