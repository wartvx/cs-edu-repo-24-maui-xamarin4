//-
using System.Text.Json;

using FirstMauiApp.Models;


namespace FirstMauiApp.Pages;

public partial class ProfilePage : ContentPage
{
    /// <summary>
    /// Модель пользовательских данных
    /// </summary>
    public UserInfo? UserInfo { get; set; }


    public ProfilePage()
    {
        InitializeComponent();
    }


    /// <summary>
    /// Вызывается до того, как элемент становится видимым
    /// </summary>
    protected override void OnAppearing()
    {
        // if (App.Current.Properties.TryGetValue("CurrentUser", out object user))
        // App.Current.Properties.Add("CurrentUser", UserInfo);
        // The Application.Current.Properties property in .NET MAUI has been removed
        //  you should use Microsoft.Maui.Storage.Preferences

        // the Preferences.Default.Set method is used to store simple data types
        //  in key-value pairs. It does not directly support storing complex objects.
        //  However, you can store complex objects by serializing them
        //  into a string format, such as JSON, and then saving that string to preferences
        var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
        string jsonUserInfo;

        // Проверяем, есть ли в словаре значение
        if (Preferences.Default.ContainsKey("CurrentUser"))
        {
            // Deserialize the object from a JSON string
            UserInfo = JsonSerializer.Deserialize<UserInfo>(
                Preferences.Default.Get("CurrentUser",
                JsonSerializer.Serialize(new UserInfo(), jsonOptions)));
        }
        else
        {
            // Добавляем, если нет
            UserInfo = new UserInfo();

            // Serialize the object to a JSON string
            jsonUserInfo = JsonSerializer.Serialize(UserInfo, jsonOptions);
            Preferences.Default.Set("CurrentUser", jsonUserInfo);
        }

        loginEntry.Text = UserInfo?.Name;
        emailEntry.Text = UserInfo?.Email;

        // Получим значения ползунков из Preferences.
        // Если значений нет - установим значения по умолчанию (false)
        gasSwitch.On = Preferences.Default.Get("gasState", false);
        climateSwitch.On = Preferences.Default.Get("climateState", false);
        electroSwitch.On = Preferences.Default.Get("electroState", false);

        base.OnAppearing();
    }


    /// <summary>
    /// Покажем уведомление с предупреждением,
    ///  если пользователю выдаются сразу все доступы
    /// </summary>
    private void NotifyUser(object sender, ToggledEventArgs e)
    {
        if (gasSwitch.On && climateSwitch.On && electroSwitch.On)
        {
            DisplayAlert("Внимание!",
                "Пользователь получит полный доступ ко всем системам",
                "OK");
        }
    }


    /// <summary>
    /// Сохраним информацию о пользователе
    /// </summary>
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        UserInfo!.Name = loginEntry.Text;
        UserInfo!.Email = emailEntry.Text;

        // Serialize the object to a JSON string
        var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
        string jsonUserInfo = JsonSerializer.Serialize(UserInfo, jsonOptions);
        Preferences.Default.Set("CurrentUser", jsonUserInfo);

        // Сохраним значения ползунков в настройки.
        Preferences.Default.Set("gasState", gasSwitch.On);
        Preferences.Default.Set("climateState", climateSwitch.On);
        Preferences.Default.Set("electroState", electroSwitch.On);

        await Shell.Current.Navigation.PopAsync();
    }
}
