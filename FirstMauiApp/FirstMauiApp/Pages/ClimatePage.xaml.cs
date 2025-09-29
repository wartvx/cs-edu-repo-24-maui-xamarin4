//-
using Microsoft.Maui.Layouts;

using FirstMauiApp.Extensions;


namespace FirstMauiApp.Pages;

public partial class ClimatePage : ContentPage
{
    public ClimatePage()
    {
        InitializeComponent();
        
        ScanOutside();
        ScanInside();
        GetPressure();
    }


    /// <summary>
    /// Внешние датчики
    /// </summary>
    public void ScanOutside()
    {
        absLayout.Add(
            // Создаем прямоугольник заданного цвета
            new BoxView { Color = Colors.LightBlue },
            // Задаем его местоположение и размеры
            new Rect(
                20, // X - координата начальной точки
                10, // Y - координата начальной точки
                100, // ширина прямоугольника
                70 // высота
            )
        );

        absLayout.Add(
            new Label
            {
                Text = $"Outside",
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 13
            },
            new Rect(20, 17, 100, 70)
        );

        absLayout.Add(
            new Label
            {
                Text = "- 15 °C",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 20
            },
            new Rect(20, 15, 100, 70)
        );
    }

    /// <summary>
    /// Внутренние датчики
    /// </summary>
    public void ScanInside()
    {
        absLayout.Add(
            new BoxView { Color = Colors.LightSalmon },
            new Rect(130, 10, 100, 70)
        );

        absLayout.Add(
            new Label
            {
                Text = $"Inside",
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 13
            },
            new Rect(130, 17, 100, 70)
        );

        absLayout.Add(
            new Label
            {
                Text = "+ 24 °C",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 20
            },
            new Rect(130, 15, 100, 70)
        );
    }


    public void GetPressure()
    {
        // Создаем новый элемент
        var pressureBox = new BoxView { Color = Colors.BurlyWood };

        /*
        // абсолютные значения позиции
        */
        // Указываем позицию (x, y, w, h)
        var position = new Rect(240, 10, 173, 70);
        // Сохраняем настройки лейаута
        AbsoluteLayout.SetLayoutBounds(pressureBox, position);
        // Устанавливаем конфигурацию (все величины абсолютные)
        AbsoluteLayout.SetLayoutFlags(pressureBox, AbsoluteLayoutFlags.None);
    
        /*
        // относительные значения позиции
        var position = new Rect(0.5, 0.5, 0.25, 0.5);
        AbsoluteLayout.SetLayoutBounds(pressureBox, position);
        AbsoluteLayout.SetLayoutFlags(pressureBox, AbsoluteLayoutFlags.All);
        */

        // Добавляем элемент
        absLayout.Children.Add(pressureBox);
    }
}
