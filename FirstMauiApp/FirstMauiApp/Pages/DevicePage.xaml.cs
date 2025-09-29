//-
using System.Text.RegularExpressions;

using FirstMauiApp.Models;


namespace FirstMauiApp.Pages;

public partial class DevicePage : ContentPage
{
    public static string PageName { get; set; } = null!;
    // public static string DeviceName { get; set; } = null!;
    // public static string? DeviceDescription { get; set; }

    // Ссылка на модель
    public HomeDevice HomeDevice { get; set; }

    public static bool CreateNew { get; set; }


    /// <summary>
    ///  Метод- конструктор принимает данные с предыдущей старницы
    /// </summary>
    public DevicePage(string pageName, HomeDevice? homeDevice = null)
    {
        PageName = pageName;

        if (homeDevice != null)
        {
            HomeDevice = homeDevice;
            CreateNew = false;
        }
        else
        {
            HomeDevice = new HomeDevice("new device name");
            CreateNew = true;
        }

        // DeviceName = HomeDevice.Name;
        // DeviceDescription = HomeDevice.Description;

        InitializeComponent();

        OpenEditor();
    }


    private void OpenEditor()
    {
        // Создание однострочного текстового поля для названия
        var newDeviceName = new Entry
        {
            BackgroundColor = Colors.AliceBlue,
            Margin = new Thickness(30, 10),
            Placeholder = "Название",
            Text = HomeDevice.Name,
            Style = (Style)Application.Current!.Resources["ValidInputStyle"]
        };
        newDeviceName.TextChanged += (sender, e) =>
            InputTextChanged(sender, e, newDeviceName);
        stackLayout.Add(newDeviceName);

        // Создание многострочного поля для описания
        var newDeviceDescription = new Editor
        {
            HeightRequest = 200,
            BackgroundColor = Colors.AliceBlue,
            Margin = new Thickness(30, 10),
            Placeholder = "Описание",
            Text = HomeDevice.Description,
            Style = (Style)Application.Current!.Resources["ValidInputStyle"]
        };
        newDeviceDescription.TextChanged += (sender, e) =>
            InputTextChanged(sender, e, newDeviceDescription);
        stackLayout.Add(newDeviceDescription);

        // Создаем заголовок для переключателя
        var switchHeader = new Label
        {
            Text = "Не использует газ",
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 5, 0, 0)
        };
        stackLayout.Children.Add(switchHeader);

        // Создаем переключатель
        Switch switchControl = new Switch
        {
            IsToggled = false,
            MinimumWidthRequest = 0,
            HorizontalOptions = LayoutOptions.Center,
            ThumbColor = Colors.DodgerBlue,
            OnColor = Colors.LightSteelBlue
        };
        stackLayout.Children.Add(switchControl);

        // Регистрируем обработчик события переключения
        switchControl.Toggled += (sender, e) =>
            SwitchHandler(sender, e, switchHeader);


        // Выбор комнаты
        var pickerHeader = new Label
        {
            Text = "Выберите комнату подключения",
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(20, 25, 0, 0)
        };
        stackLayout.Children.Add(pickerHeader);

        var roomPicker = new Picker
        {
            Margin = new Thickness(30, 0)
        };
        roomPicker.Items.Add("Кухня");
        roomPicker.Items.Add("Ванная");
        roomPicker.Items.Add("Гостиная");
        roomPicker.SelectedItem = roomPicker.Items
            .FirstOrDefault(i => i == HomeDevice.Room);
        stackLayout.Children.Add(roomPicker);

        roomPicker.SelectedIndexChanged += (sender, eventArgs) =>
            RoomPicker_SelectedIndexChanged(sender, eventArgs, roomPicker);


        // Кнопка сохранения с обработчиками
        var addButton = new Button
        {
            Text = "Сохранить",
            Margin = new Thickness(30, 10),
            BackgroundColor = Colors.Silver
        };

        addButton.Clicked += (sender, eventArgs) =>
            AddButtonClicked(sender, eventArgs, new View[] {
                newDeviceName,
                newDeviceDescription,
                switchControl
        });

        stackLayout.Add(addButton);

        // Добавляем кнопку перехода на страницу с инструкцией и её обработчик
        var userManualButton = new Button
        {
            Text = "Инструкция по эксплуатации",
            Margin = new Thickness(30, 10),
            BackgroundColor = Colors.Silver
        };

        userManualButton.Clicked += (sender, eventArgs) =>
            ManualButtonClicked(sender, eventArgs);

        stackLayout.Add(userManualButton);
    }


    /// <summary>
    /// Обработка переключателя
    /// </summary>
    public void SwitchHandler(object? sender, ToggledEventArgs e, Label header)
    {
        if (!e.Value)
        {
            header.Text = "Не использует газ";
            return;
        }

        header.Text = "Использует газ";
    }


    /// <summary>
    /// Обработчик-валидатор текстовых полей
    /// </summary>
    private void InputTextChanged(
        object? sender, TextChangedEventArgs e, InputView view)
    {
        // Регулярное выражение для проверки введенных значений
        // только буквы, цифры, пробелы. начиная с буквы. латиница
        // Regex rgx = new Regex("^[a-zA-Z]+[a-zA-Z0-9\\s]*$");
        // только буквы, цифры, пробелы. начиная с буквы. латиница, кириллица, подчеркивания
        Regex rgx = new Regex("^\\p{L}+[\\w\\s]*$");
        // Не разрешаем использовать специальные символы в названии и описании,
        //  если они есть, то проставляем Invalid
        VisualStateManager.GoToState(view, rgx.IsMatch(view.Text) ? "Valid" : "Invalid");

        if (view is Entry)
        {
            HomeDevice.Name = view.Text;
        }
        else
        {
            HomeDevice.Description = view.Text;
        }
    }


    /// <summary>
    /// Обновляем комнату в модели
    /// </summary>
    private void RoomPicker_SelectedIndexChanged(
        object? sender, EventArgs e, Picker picker)
    {
        HomeDevice.Room = picker.Items[picker.SelectedIndex];
    }


    /// <summary>
    /// Кнопка сохранения деактивирует все контролы
    /// </summary>
    private async void AddButtonClicked(object? sender, EventArgs e, View[] views)
    {
        if (string.IsNullOrEmpty(HomeDevice.Room))
        {
            await DisplayAlert("Выберите комнату",
                $"Комната подключения не выбрана!", "ОК");
            return;
        }

        // Деактивируем все контролы
        foreach (var view in views)
            view.IsEnabled = false;

        // HomeDevice.Name = DeviceName;
        // HomeDevice.Description = DeviceDescription;

        if (CreateNew)
        {
            // Если нужно создать новое - то сначала выполним проверку,
            //  не существует ли ещё такое
            var existingDevices = await App.HomeDevices.GetHomeDevices();
            if (existingDevices.Any(d => d.Name == HomeDevice.Name))
            {
                await DisplayAlert("Ошибка",
                    $"Устройство {HomeDevice.Name} уже подключено."
                    + $"{Environment.NewLine}Выберите другое имя.", "ОК");
            }
            else
            {
                var newDeviceDto = App.Mapper.Map<Data.HomeDeviceEntity>(HomeDevice);
                await App.HomeDevices.AddHomeDevice(newDeviceDto);

                /*
                // Пример другого способа навигации - с помощью удаления
                //  предыдущей страницы из стека и "вставки"
                //  (дано для демонстрации возможностей)
                Shell.Current.Navigation.RemovePage(
                    Shell.Current.Navigation.NavigationStack[
                        Shell.Current.Navigation.NavigationStack.Count - 2]);
                Shell.Current.Navigation.InsertPageBefore(new DeviceListPage(), this);
                await Shell.Current.Navigation.PopAsync();
                */
            }
            return;
        }

        var updatedDevice = App.Mapper.Map<Data.HomeDeviceEntity>(HomeDevice);
        await App.HomeDevices.UpdateHomeDevice(updatedDevice);
        await Shell.Current.Navigation.PopAsync();
    }


    /// <summary>
    /// Переход на страницу с инструкцией
    /// </summary>
    private async void ManualButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(
            new DeviceManualPage(HomeDevice.Name, HomeDevice.Id));
    }
}
