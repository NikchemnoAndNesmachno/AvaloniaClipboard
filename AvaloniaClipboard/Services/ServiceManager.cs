using System;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaClipboard.Services;

public static class ServiceManager
{
    private static IServiceProvider Provider { get; set; }
    public static ServiceCollection Services { get; set; } = [];

    public static void Init()
    {
    }

    public static T Get<T>() where T : notnull
    {
        return Provider.GetRequiredService<T>();
    }
}