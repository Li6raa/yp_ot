using Avalonia.Media.Imaging;

namespace yp_ot;

public class Услуги
{
    public int ID { get; set; }
    public string Название{ get; set; }
    public int Стоимость { get; set; }
    public int Скидка { get; set; }
    public string Продолжительность { get; set; }
    public Bitmap? Изображение { get; set; }
    public string путь { get; set; }
    public double Цена_со_скидкой { get; set; }
}