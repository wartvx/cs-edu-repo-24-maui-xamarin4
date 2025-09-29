namespace FirstMauiApp.Pages;

public partial class RegisterPage : ContentPage
{
    // константа для текста кнопки
    public const string BUTTON_TEXT = "Регистрация";

    int count = 0;

    public RegisterPage()
    {
        InitializeComponent();
        
        PlatformAdjust();
    }

    private void OnRegisterClicked(object? sender, EventArgs e)
    {
        count++;

        /*
		if (count == 1)
			registerBtn.Text = $"Clicked {count} time";
		else
			registerBtn.Text = $"Clicked {count} times";
		*/

        /*
		registerBtn.Text = "Выполняется регистрация...";
		*/

        // динамическая загрузка интерфейса из XML-кода
        // при использовании такого подхода производительность будет снижаться,
        //  поэтому делать это стоит лишь при необходимости
        string xaml = "<Button Text=\"⌛ Выполняется регистрация..\"  />";
        registerBtn.LoadFromXaml(xaml);

        SemanticScreenReader.Announce(registerBtn.Text);
    }

    /// <summary>
    /// Настраиваем вид для разных платформ
    /// </summary>
    public void PlatformAdjust()
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            placeHolder.PlaceholderColor = Colors.SlateGray;
            registerBtn.TextColor = Colors.AliceBlue;
            registerBtn.Margin = new Thickness(0, 5);

            var backgroundImageSource = ImageSource.FromFile("home_background_winui.jpg");
            BackgroundImageSource = backgroundImageSource;
        }

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            var backgroundImageSource = ImageSource.FromFile("home_background_android.jpg");
            BackgroundImageSource = backgroundImageSource;
        }
    }
}
