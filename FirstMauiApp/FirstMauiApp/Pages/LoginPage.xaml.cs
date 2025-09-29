namespace FirstMauiApp.Pages;

public partial class LoginPage : ContentPage
{
    // константа для текста кнопки
    public const string BUTTON_TEXT = "Войти";

    int count = 0;

    public LoginPage()
    {
        InitializeComponent();

        // Изменяем внешний вид кнопки для Desktop-версии
        // if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
        //     loginBtn.CornerRadius = 0;

        infoDeviceLabel.Text = $"Запущено на устройстве {DeviceInfo.Current.Name}  ({DeviceInfo.Current.Manufacturer}) {DeviceInfo.Current.Model}";
    }

    private void OnLoginClicked(object? sender, EventArgs e)
    {
        // LoginClickWithCounter();
        LoginClickWithNavigation();

        SemanticScreenReader.Announce(loginBtn.Text);
    }


    private async void LoginClickWithNavigation()
    {
        loginBtn.Text = "Выполняется вход...";

        // Имитация задержки (приложение загружает данные с сервера)
        await Task.Delay(150);

        // Переход на следующую страницу - страницу списка устройств
        await Shell.Current.Navigation.PushAsync(new DeviceListPage());

        // Восстановим первоначальный текст на кнопке
        //  (на случай, если пользователь вернется на этот экран
        //  чтобы выполнить вход снова)
        loginBtn.Text = BUTTON_TEXT;
    }


    private void LoginClickWithCounter()
    {
        count++;

        /*
		if (count == 1)
			loginBtn.Text = $"Clicked {count} time";
		else
			loginBtn.Text = $"Clicked {count} times";
		*/

        /*
		loginBtn.Text = "Выполняется вход...";
		*/

        /*
		// динамическая загрузка интерфейса из XML-кода
		// при использовании такого подхода производительность будет снижаться,
		//  поэтому делать это стоит лишь при необходимости
		string xaml = "<Button Text=\"⌛ Выполняется вход..\"  />";
		loginBtn.LoadFromXaml(xaml);
		*/

        if (count == 0)
        {
            // Если первая попытка - просто меняем сообщения
            loginBtn.Text = $"Выполняется вход..";
        }
        else if (count > 5) // Слишком много попыток - показываем ошибку
        {
            // Деактивируем кнопку
            loginBtn.IsEnabled = false;

            /*
            // Показываем текстовое сообщение об ошибке
            infoMessageLabel.Text = "Слишком много попыток! Попробуйте позже.";
            */

            // Получаем последний дочерний элемент, используя свойство Children, затем выполняем распаковку
            var infoMessageLabel1 = (Label)verticalStackLayout.Children.Last();
            // Задаем текст элемента
            infoMessageLabel1.Text = "Слишком много попыток! Попробуйте позже.";


            // Добавляем еще один элемент через свойство Children
            //  применяем к нему стиль из словаря ресурса
            var infoMessageLabel2 = new Label
            {
                Text = "Уж очень много попыток! Попробуйте позже.",


                // TextColor = Colors.Red,
                // FontSize = 14,

                // https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/resource-dictionaries?view=net-maui-9.0
                // https://metanit.com/sharp/maui/5.1.php

                // устанавливаем статический ресурс для элемента

                // используем ресурсы уровня отдельного элемента (VerticalStackLayout)
                // TextColor = (Color)this.Content.Resources["errorColor"],
                // FontSize = (double)this.Content.Resources["errorFont"],
                // TextColor = (Color)verticalStackLayout.Resources["errorColor"],
                // FontSize = (double)verticalStackLayout.Resources["errorFont"],

                // используем ресурсы уровня текущей страницы (ContentPage)
                // TextColor = (Color)this.Resources["errorColor"],
                // FontSize = (double)this.Resources["errorFont"],

                // используем ресурсы уровня приложения (App.xaml)
                // TextColor = (Color)Application.Current!.Resources["errorColor"],
                // FontSize = (double)Application.Current!.Resources["errorFont"],

                // используем stand-alone словарь (.xaml)
                // can be consumed by merging it into another resource dictionary
                // подключаем словарь на нужный уровень либо по пути, либо по имени
                // TextColor = (Color)this.Resources["errorColor"],
                // FontSize = (double)this.Resources["errorFont"],


                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = new Thickness()
                {
                    Bottom = 30,
                    Left = 10,
                    Right = 10,
                    Top = 30
                }
            };

            verticalStackLayout.Children.Add(infoMessageLabel2);


            // устанавливаем динамический ресурс для элемента
            infoMessageLabel2.SetDynamicResource(Label.TextColorProperty, "errorColor");

            // обновляем динамический ресурс
            // элементы, для которых заданы [{DynamicResource Key=errorColor}]
            //  обновятся динамически
            // сам ресурс описан обычным образом в .xaml на уровне текущей страницы (ContentPage)
            // this.Resources["errorColor"] = Color.FromArgb("#1976D2");
            // сам ресурс описан обычным образом в .xaml на уровне приложения (App.xaml)
            Application.Current!.Resources["errorColor"] = Color.FromArgb("#1976D2");
        }
        else
        {
            // Изменяем текст кнопки и показываем количество попыток входа
            loginBtn.Text = $"Выполняется вход...   Попыток входа: {count}";
        }
    }
}
