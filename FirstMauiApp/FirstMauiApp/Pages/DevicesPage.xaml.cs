//-
using Microsoft.Maui.Controls.Shapes;

using FirstMauiApp.Models;


namespace FirstMauiApp.Pages;

public partial class DevicesPage : ContentPage
{
    public DevicesPage()
    {
        InitializeComponent();
        
        GetDevices();
    }


    void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Console.WriteLine($"ScrollX: {e.ScrollX}, ScrollY: {e.ScrollY}");
    }


    private void OnButtonClicked(object? sender, EventArgs e)
    {
        Console.WriteLine($"ButtonClicked");
        ScrollAsync();
    }


    private async void ScrollAsync()
    {
        var lastChild = scrollView.Children.LastOrDefault();
        if (lastChild != null)
        {
            await scrollView.ScrollToAsync(lastChild, ScrollToPosition.End, true);
        }
    }


    /// <summary>
    /// Метод для выгрузки устройств
    /// </summary>
    public void GetDevices()
    {
        // Создадим некий список устройств
        // В реальном приложении они могут доставаться из базы или веб-сервиса

        /*
        var homeDevices = new List<string>()
        {
            "Чайник",
            "Стиральная машина",
            "Посудомоечная машина",
            "Мультиварка",
            "Водонагреватель",
            "Плита",
            "Микроволновая печь",
            "Духовой шкаф",
            "Холодильник",
            "Увлажнитель воздуха",
            "Телевизор",
            "Пылесос",
            "Музыкальный центр",
            "Компьютер",
            "Игровая консоль"
        };
        */

        List<HomeDevice> homeDevices = [];

        // Заполняем список устройств
        homeDevices.Add(new HomeDevice("Чайник", "kettle.jpeg"));
        homeDevices.Add(new HomeDevice("Стиральная машина"));
        homeDevices.Add(new HomeDevice("Посудомоечная машина"));
        homeDevices.Add(new HomeDevice("Мультиварка"));
        homeDevices.Add(new HomeDevice("Водонагреватель"));
        homeDevices.Add(new HomeDevice("Плита"));
        homeDevices.Add(new HomeDevice("Микроволновая печь"));
        homeDevices.Add(new HomeDevice("Духовой шкаф"));
        homeDevices.Add(new HomeDevice("Холодильник"));
        homeDevices.Add(new HomeDevice("Увлажнитель воздуха"));
        homeDevices.Add(new HomeDevice("Телевизор"));
        homeDevices.Add(new HomeDevice("Пылесос"));
        homeDevices.Add(new HomeDevice("музыкальный центр"));
        homeDevices.Add(new HomeDevice("Компьютер"));
        homeDevices.Add(new HomeDevice("Игровая консоль"));


        // Создадим новый стек
        var innerStack = new VerticalStackLayout();

        // Сохраним в стек имеющиеся данные, используя свойство Children
        foreach (var homeDevice in homeDevices)
        {
            // Создадим текстовый элемент
            var deviceLabel = new Label() { Text = $"  {homeDevice.Name}", FontSize = 17 };

            // innerStack.Children.Add(new Label());
            // innerStack.Children.Add(deviceLabel);

            // Контейнер Frame, внутри которого будет отображаться текстовый элемент
            // Frame is obsolete as of .NET 9. Please use Border instead
            // var frame = new Frame()
            // {
            //     BorderColor = Colors.Gray,
            //     BackgroundColor = Color.FromHex("#e1e1e1"),
            //     CornerRadius = 4,
            //     Margin = new Thickness(10, 1)
            // };
            var frame = new Border()
            {
                Stroke = Colors.Gray,
                BackgroundColor = Color.FromArgb("#e1e1e1"),
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(40, 0, 0, 40)
                },
                Margin = new Thickness(10, 1)
            };
            // Задаем содержимое контейнера Frame
            frame.Content = deviceLabel;

            //***

            // Создаем объект, отвечающий за распознавание нажатий
            var gesture = new TapGestureRecognizer();
            // Устанавливаем по событию нажатия вызов метода  ShowImage(...)
            //  со ссылкой на изображение в качестве аргумента
            gesture.Tapped += async (sender, e) => await ShowImage(sender, e, homeDevice.Image);
            // Добавляем настроенный распознаватель нажатий в текущий фрейм
            frame.GestureRecognizers.Add(gesture);

            //***

            // Добавляем фреймы в стек для их отображения в едином списке по порядку
            innerStack.Children.Add(frame);
        }

        // Сохраним стек внутрь уже имеющегося в xaml-файле блока прокручиваемой разметки
        scrollView.Content = innerStack;
    }


    /// <summary>
    /// Показ изображения по нажатию
    /// </summary>
    public async Task ShowImage(object? sender, EventArgs e, string? imageName)
    {
        // объект изображения
        Image image;

        // Если изображение отсутствует - показываем бланк изображения
        // При наличии изображения - загружаем его по заданному пути

        if (string.IsNullOrEmpty(imageName))
        {
            // показываем информационное окно
            await DisplayAlert("", "Изображение устройства отсутствует", "OK");
            // return;

            // Загрузка файла из сети

            // Подключаем удаленный ресурс в качестве источника изображения
            /*
            // UriImageSource uriImage = ImageSource.FromUri(new Uri("https://i.sstatic.net/y9DpT.jpg"));
            UriImageSource uriImage = new UriImageSource
            {
                Uri = new Uri("https://i.sstatic.net/y9DpT.jpg"),
                CachingEnabled = false
                // CacheValidity = new TimeSpan(10,0,0,0)
            };
            image = new Image
            {
                Source = uriImage
            };
            */

            /*
            image = new Image
            {
                Source = ImageSource.FromUri(new Uri("https://i.sstatic.net/y9DpT.jpg"))
            };
            */

            // при загрузке изображения по Uri для Windows не работает загрузка,
            //  если для пути источника требуется обязательное присутствие
            //  символов в верхнем регистре (y9DpT.jpg != y9dpt.jpg)
            // для Android загрузка по пути источнка с верхним регистром работает корректно
            // при загрузке как локального файла, так и ресурса (встроенного изображения)
            //  из [\Resources\Images] необходимо, чтобы сам файл в папке
            //  имел имя в нижнем регистре (иначе, возникает ошибка компиляции)
            //  обращение в коде к этому файлу может быть в произвольном регистре
            // File names must be lowercase, start and end with a letter character,
            //  and contain only alphanumeric characters or underscores

            image = new Image();
            // image.Source = ImageSource.FromUri(new Uri("https://aka.ms/campus.jpg")); // +
            // image.Source = ImageSource.FromUri(new Uri("https://aka.ms/Campus.jpg")); // +
            // image.Source = ImageSource.FromUri(new Uri("https://i.sstatic.net/y9DpT.jpg")); // -
            // image.Source = ImageSource.FromUri(new Uri("https://i.sstatic.net/y9dpt.jpg")); // -
            // image.Source = ImageSource.FromFile($"y9DpT.jpg"); // -
            image.Source = ImageSource.FromFile($"y9dpt.jpg"); // +
        }
        else
        {
            // Локально размещенные изображения
            // image = new Image
            // {
            //     Source = ImageSource.FromFile($"{imageName}")
            // };

            // Встроенные изображения
            image = new Image
            {
                Source = ImageSource.FromResource($"FirstMauiApp.Resources.Images.{imageName}")
            };
        }

        // Инициализируем страницу
        Content = image;
    }

}
