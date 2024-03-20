using System.Net;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Mysqlx.Crud;

namespace yp_ot;

public partial class MainWindow : Window
{ 
    public MainWindow()
    {
        InitializeComponent();
    }
   
    private void OpenNewWindow(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new ServicesForUser();
        newWindow.Show();
        this.Close();
    }
    private void Auto(object? sender, RoutedEventArgs routedEventArgs)
    {
        authorization back = new authorization();
        back.Show();
        this.Close();
    }
  
    private void exit(object? sender, RoutedEventArgs routedEventArgs)
    {
        this.Close();
    }
}