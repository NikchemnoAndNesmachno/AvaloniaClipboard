using System;
using AvaloniaClipboard.Views;
using AvaloniaClipboard.Views.ServiceImplements;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaClipboard.Services;

public static class ServiceManager
{
    private static IServiceProvider Provider { get; set; }
    private static ServiceCollection Services { get; set; } = [];

    public static void RegisterSingleton<TInterface, TImplementation>() 
        where TImplementation : class, TInterface 
        where TInterface : class =>
        Services.AddSingleton<TInterface, TImplementation>();

    public static void RegisterSingleton<TImplementation>() 
        where TImplementation : class =>
        Services.AddSingleton<TImplementation>();

    public static void RegisterTransient<TImplementation>() 
        where TImplementation : class =>
        Services.AddTransient<TImplementation>();
    public static void Init()
    {
        Provider = Services.BuildServiceProvider();
    }
    public static T Get<T>() where T: notnull => Provider.GetRequiredService<T>();
}