using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace yp_ot;

public partial class authorization : Window
{
    public authorization()
    {
        InitializeComponent();
    }
    private void exit(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new MainWindow();
        newWindow.Show();
        this.Close();
    }
    private void OpenAdminVersion(object? sender, RoutedEventArgs routedEventArgs)
    {
        if (Login.Text == "Admin" && Password.Text == "Admin")
        {
            var newWindow = new main_admin();
            newWindow.Show();
            this.Close();
        }
        
    }
}