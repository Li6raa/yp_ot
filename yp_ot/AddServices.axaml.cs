using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace yp_ot;

public partial class AddServices : Window
{
    private List<Услуги> _Услугиs;
    private Услуги CurrentServ;
    private MySqlConnection conn;
    private string _connString="server=localhost; database=pp_ot; port=3306;User Id=root;password=root";
    public AddServices(Услуги currentServ, List<Услуги> _услугиs)
    {
        InitializeComponent();
        CurrentServ = currentServ;
        this.DataContext = currentServ;
        _Услугиs = _услугиs;
        FillCmb();
    }
    
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = _Услугиs.FirstOrDefault(x => x.ID == CurrentServ.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(_connString);
                conn.Open();
                string add =
                    "INSERT INTO услуги (Название, Стоимость, Скидка, Продолжительность, Изображение) VALUES ('" +
                    Name.Text + "', '" + Convert.ToDouble(Price.Text) + "', '"  +
                    Convert.ToInt32(Discount.Text) + "', '" + Long.Text + " " + TimeComboBox.SelectedValue + "', '" + Img.Text + "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Services back = new Services();
                back.Show();
                this.Close();
                
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(_connString);
                conn.Open();
                string upd = "UPDATE услуги SET Название = '" + Name.Text +
                             "', Стоимость = '" + Convert.ToInt32(Price.Text) + "', Скидка = '" +
                             Convert.ToInt32(Discount.Text) +"', Продолжительность = '" +
                             Long.Text + " " + TimeComboBox.SelectedValue +  "', Изображение = '" + Img.Text + "'  WHERE ID = " + Convert.ToInt32(ID.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Services back = new Services();
                back.Show();
                this.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Services back = new Services();
        back.Show();
        this.Close();
    }

    private async void File_Select(object sender, RoutedEventArgs e)
    {
        try
        {
            OpenFileDialog fileDialog = new OpenFileDialog(); //создание диалогового окна выбора файла
            fileDialog.Filters.Add(new FileDialogFilter() { Name = "Image Files", Extensions = { "jpg", "jpeg", "png", "gif" } }); //ограничение на выбор только изображений
            string[]? fileNames = await fileDialog.ShowAsync(this); //отображение диалогового окна и получение выбранных файлов
            if (fileNames != null && fileNames.Length > 0) //если файл выбран
            {
                string imagePath = System.IO.Path.GetFileName(fileNames[0]); //получение пути к выбранному файлу
                Img.Text = imagePath;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    public void FillCmb()
    {
        var discontComboBox = this.Find<ComboBox>("TimeComboBox");
        discontComboBox.ItemsSource = new List<string>
        {
            "минут",
            "минуты",
            "часа",
            "часов"
        };
    }
}