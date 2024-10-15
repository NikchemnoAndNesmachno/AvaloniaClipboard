using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using AvaloniaClipboard.Services;

namespace AvaloniaClipboard.Views.ServiceImplements;

public abstract class BaseResourceChanger
{
    protected virtual IList<string> Values { get; set; } = [];
    protected virtual int ResourceIndex { get; set; } = 0; 
    protected IResourceDictionary Resources => ((App)Application.Current!).Resources;

    protected IResourceProvider ResourceDictionary
    {
        get => Resources.MergedDictionaries[ResourceIndex];
        set => Resources.MergedDictionaries[ResourceIndex] = value;
    }

    protected void SetValue(int index) => 
        ResourceDictionary = new ResourceInclude(new Uri(Process(Values[index])));

    protected abstract string Process(string value);
}