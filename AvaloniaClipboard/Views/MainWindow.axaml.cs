using System;
using Avalonia.Controls;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels;
using AvaloniaClipboard.Views.ServiceImplements;

namespace AvaloniaClipboard.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Closing += Window_OnClosing;
    }

    protected override void OnInitialized()
    {
        DataContext = ServiceManager.Get<MainWindowViewModel>();
      
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        ((Window)sender).Hide();
        e.Cancel = true;
 
    }
    
}