using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace yp_ot;

public partial class RecordUser : Window
{
    public RecordUser()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    string connStr = "server=localhost;database=pp_ot;port=3306;User Id=root;password=root";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            string add = "INSERT INTO записи (Электронный_адрес_клиента, Дата_приема, Описание_проблемы) VALUES ('" + Start.Text + "', '" + Date_Pos.Text + "', '" + Problem.Text + "');";
            MySqlCommand cmd = new MySqlCommand(add, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            ServicesForUser back = new ServicesForUser();
            back.Show();
            this.Close();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        ServicesForUser back = new ServicesForUser();
        this.Close();
        back.Show();
    }
}