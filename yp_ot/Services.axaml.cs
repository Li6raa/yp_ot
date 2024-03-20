using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows;
using Avalonia.Data.Converters;
using Avalonia.Utilities;
using System.Globalization;

namespace yp_ot;

public partial class Services : Window
{
    private List<Услуги> _услугис;
    string fullTable = "SELECT * FROM услуги";
    private string _connString="server=localhost; database=pp_ot; port=3306;User Id=root;password=root";
    private MySqlConnection conn;
    public Services()
    {
        InitializeComponent();
        ShowTable(fullTable);
        FillCmb();
    }
    public void ShowTable(string sql)
    {
        _услугис = new List<Услуги>();
        conn = new MySqlConnection(_connString);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Serv = new Услуги()
            {
                ID = reader.GetInt32("ID"),
                Название= reader.GetString("Название"),
                Стоимость= reader.GetInt32("Стоимость"),
                Скидка = reader.GetInt32("Скидка"),
                Продолжительность= reader.GetString("Продолжительность"),
                Изображение = LoadImage("avares://yp_ot/фото_услуг/" + reader.GetString("Изображение")),
                путь = reader.GetString("Изображение"),
                Цена_со_скидкой = reader.GetInt32("Стоимость") - (reader.GetInt32("Стоимость") * reader.GetInt32("Скидка") / 100)
            };
            _услугис.Add(Serv);
        }
            
        conn.Close();
        DataGrid.ItemsSource = _услугис;
        DataGrid.LoadingRow += DataGrid_LoadingRow;
    }

    public Bitmap LoadImage(string Uri)
    {
        return new Bitmap(AssetLoader.Open(new Uri(Uri)));
    }

    private void SearchService(object? sender, TextChangedEventArgs e)
    {
        var srv = _услугис;
        srv = srv.Where(x => x.Название.Contains(ServSearch.Text)).ToList();
        DataGrid.ItemsSource = srv;
    }
    public void FillCmb()
    {
        _услугис = new List<Услуги>();
        conn = new MySqlConnection(_connString);
        conn.Open();
        MySqlCommand command = new MySqlCommand(fullTable, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Serv = new Услуги()
            {
                ID = reader.GetInt32("ID"),
                Название= reader.GetString("Название"),
                Стоимость= reader.GetInt32("Стоимость"),
                Скидка = reader.GetInt32("Скидка"),
                Продолжительность= reader.GetString("Продолжительность"),
                Изображение = LoadImage("avares://yp_ot/фото_услуг/" + reader.GetString("Изображение")),
                путь = reader.GetString("Изображение"),
                Цена_со_скидкой = reader.GetInt32("Стоимость") - (reader.GetInt32("Стоимость") * reader.GetInt32("Скидка") / 100)
            };
            _услугис.Add(Serv);
        }
        conn.Close();
            
        var discontComboBox = this.Find<ComboBox>("DiscontComboBox");
        discontComboBox.ItemsSource = new List<string>
        {
            "Все скидки",
            "От 0% до 5%",
            "От 5% до 15%",
            "От 15% до 30%",
            "От 30% до 70%"
        };
    }

    private void SortAscending(object? sender, RoutedEventArgs e)
    {
        var sortedItems = DataGrid.ItemsSource.Cast<Услуги>().OrderBy(s => s.Стоимость).ToList();
        DataGrid.ItemsSource = sortedItems;
    }
    private void SortDescending(object? sender, RoutedEventArgs e)
    {
        var sortedItems = DataGrid.ItemsSource.Cast<Услуги>().OrderByDescending(s => s.Стоимость).ToList();
        DataGrid.ItemsSource = sortedItems;
    }
    private void DiscountFilter(object? sender, SelectionChangedEventArgs e)
    {
        var discontComboBox = (ComboBox)sender;
        var selectedDiscount = discontComboBox.SelectedItem as string;
            
        int startDiscount = 0;
        int endDiscount = 0;
            
        if (selectedDiscount == "Все скидки")
        {
            DataGrid.ItemsSource = _услугис;
        }
        else if (selectedDiscount == "От 0% до 5%")
        {
            startDiscount = 1;
            endDiscount = 5;
        }
        else if (selectedDiscount == "От 5% до 15%")
        {
            startDiscount = 5;
            endDiscount = 15;
        }
        else if (selectedDiscount == "От 15% до 30%")
        {
            startDiscount = 15;
            endDiscount = 30;
        }
        else if (selectedDiscount == "От 30% до 70%")
        {
            startDiscount = 30;
            endDiscount = 71;
        }
            
        if (startDiscount != 0 && endDiscount != 0)
        {
            var filteredUsers = _услугис
                .Where(x => x.Скидка >= startDiscount && x.Скидка < endDiscount)
                .ToList();

            DataGrid.ItemsSource = filteredUsers;
        }
    }
  
    private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
    {
        Услуги _услугис = e.Row.DataContext as Услуги; 
        if (_услугис != null) 
        {
            if (0 <= _услугис.Скидка && _услугис.Скидка < 5) 
            {
                e.Row.Background = Brushes.DarkGreen; 
            }
            else if (5 <= _услугис.Скидка && _услугис.Скидка < 15) 
            {
                e.Row.Background = Brushes.Green;
            }
            else if (15 <= _услугис.Скидка && _услугис.Скидка < 30) 
            {
                e.Row.Background = Brushes.LimeGreen;
            }
            else if (30 <= _услугис.Скидка && _услугис.Скидка <= 70) 
            {
                e.Row.Background = Brushes.PaleGreen;
            }
            else
            {
                e.Row.Background = Brushes.Transparent; 
            }
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        Услуги newServ = new Услуги();
        AddServices add = new AddServices(newServ, _услугис);
        add.Title = "Добавление данных";
        add.Zag.Text = "Добавление данных";
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        Услуги currentServ = DataGrid.SelectedItem as Услуги;
        if (currentServ == null)
            return;
        AddServices edit = new AddServices(currentServ, _услугис);
        edit.Title = "Редактирование данных";
        edit.Zag.Text = "Редактирование данных";
        edit.Show();
        this.Close();
    }

    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        try
        {
            Услуги srv = DataGrid.SelectedItem as Услуги;
            if (srv == null)
            {
                return;
            }
            conn = new MySqlConnection(_connString);
            conn.Open();
            string sql = "DELETE FROM услуги WHERE ID = " + srv.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            _услугис.Remove(srv);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        ShowTable(fullTable);
    }
    private void Main(object? sender, RoutedEventArgs e)
    {
        MainWindow logout = new MainWindow();
        logout.Show();
        this.Close();
    }
}