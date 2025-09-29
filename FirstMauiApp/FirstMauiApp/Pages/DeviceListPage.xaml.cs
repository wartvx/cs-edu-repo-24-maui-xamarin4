//-
using System.Collections.ObjectModel;

using FirstMauiApp.Models;


namespace FirstMauiApp.Pages;

public partial class DeviceListPage : ContentPage
{
    // public List<string> Devices { get; set; } = [];
    // public List<HomeDevice> Devices { get; set; } = [];
    // public ObservableCollection<HomeDevice> Devices { get; set; } = [];

    // группируемая коллекция
    public ObservableCollection<Group<string, HomeDevice>> DeviceGroups
    { get; set; } = [];

    // Ссылка на выбранный объект
    HomeDevice? SelectedDevice;


    public DeviceListPage()
    {
        InitializeComponent();

        // Shell.SetNavBarIsVisible(this, false);

        // Заполняем список устройств

        /*
        Devices.Add("Чайник");
        Devices.Add("Стиральная машина");
        Devices.Add("Посудомоечная машина");
        Devices.Add("Мультиварка");
        Devices.Add("Водонагреватель");
        Devices.Add("Плита");
        Devices.Add("Микроволновая печь");
        Devices.Add("Духовой шкаф");
        Devices.Add("Холодильник");
        Devices.Add("Увлажнитель воздуха");
        Devices.Add("Телевизор");
        Devices.Add("Пылесос");
        Devices.Add("музыкальный центр");
        Devices.Add("Компьютер");
        Devices.Add("Игровая консоль");
        */

        /*
        Devices.Add(new HomeDevice("Чайник", description: "LG, объем 2л.",
            image: "chainik.png"));
        Devices.Add(new HomeDevice("Стиральная машина", description: "BOSCH",
            image: "stiralnayamashina.png"));
        Devices.Add(new HomeDevice("Посудомоечная машина", description: "Gorenje",
            image: "posudomoechnayamashina.png"));
        Devices.Add(new HomeDevice("Мультиварка", description: "Philips",
            image: "multivarka.png"));
        */

        /*
        // Первоначальные данные сохраним в обычном листе
        var initialList = new List<HomeDevice>();
        initialList.Add(new HomeDevice("Чайник",
            image: "Chainik.png",
            description: "LG, объем 2л.", "Кухня")
            { Id = new Guid("735a3848-dad1-40f6-8fb7-36d2da16f1f3") }
            );
        initialList.Add(new HomeDevice("Стиральная машина",
            image: "StiralnayaMashina.png",
            description: "BOSCH", "Ванная")
            { Id = new Guid("cca22a07-5593-4485-b21d-ed7aba4ac815") }
            );
        initialList.Add(new HomeDevice("Посудомоечная машина",
            image: "PosudomoechnayaMashina.png",
            description: "Gorenje", "Кухня")
            { Id = new Guid("eba7c7eb-941b-4b1d-a179-21fa87a783ac") }
            );
        initialList.Add(new HomeDevice("Мультиварка",
            image: "Multivarka.png",
            description: "Philips", "Кухня")
            { Id = new Guid("7e19330d-23f2-4fe7-b8b7-14d3c8163e73") }
            );

        // Сгруппируем по комнатам
        var devicesByRooms = initialList.GroupBy(d => d.Room)
            .Select(g => new Group<string, HomeDevice>(g.Key!, g));

        // Сохраним
        DeviceGroups = new ObservableCollection<Group<string, HomeDevice>>(devicesByRooms);
        */

        /*
        BindingContext = this;
        */
    }


    protected async override void OnAppearing()
    {
        // Загрузка данных из базы
        var devicesFromDb = await App.HomeDevices.GetHomeDevices();
        // Мапим сущности БД в сущности бизнес-логики
        var deviceModelList = App.Mapper.Map<Models.HomeDevice[]>(devicesFromDb);

        // Сгруппируем по комнатам
        var devicesByRooms = deviceModelList.GroupBy(d => d.Room)
            .Select(g => new Group<string, HomeDevice>(g.Key!, g));

        // Сохраним
        DeviceGroups = [];
        DeviceGroups = new ObservableCollection<Group<string, HomeDevice>>(devicesByRooms);

        BindingContext = this;

        base.OnAppearing();
    }


    /// <summary>
    /// Обработчик нажатия
    /// </summary>
    private void deviceList_ItemTapped(
        object sender, ItemTappedEventArgs e)
    {
        // DisplayAlert("Нажатие", $"Вы нажали на элемент {e.Item}", "OK");

        // распаковка модели из объекта
        var tappedDevice = (HomeDevice)e.Item;
        // уведомление
        DisplayAlert("Нажатие", $"Вы нажали на элемент {tappedDevice.Name}", "OK");
    }


    /// <summary>
    /// Обработчик выбора
    /// </summary>
    private void deviceList_ItemSelected(
        object sender, SelectedItemChangedEventArgs e)
    {
        // DisplayAlert("Выбор", $"Вы выбрали {e.SelectedItem}", "OK");

        // распаковка модели из объекта
        SelectedDevice = (HomeDevice)e.SelectedItem;

        // уведомление
        DisplayAlert("Выбор", $"Вы выбрали {SelectedDevice.Name}", "OK");
    }


    /// <summary>
    /// Обработчик добавления нового устройства
    /// </summary>
    private async void AddDevice(object sender, EventArgs e)
    {
        /*

        // Запрос и валидация имени устройства
        var newDeviceName = await DisplayPromptAsync("Новое устройство",
            "Введите имя устройства", "Продолжить", "Отмена");

        if (newDeviceName is null)
        {
            await DisplayAlert("Отмена", $"Отмена", "ОК");
            return;
        }

        if (Devices.Any(d => d.Name.CompareTo(newDeviceName.Trim()) == 0))
        {
            await DisplayAlert("Ошибка", $"Устройство '{newDeviceName}' уже существует", "ОК");
            return;
        }

        // Запрос описания устройства
        var newDeviceDescription = await DisplayPromptAsync(newDeviceName,
            "Добавьте краткое описание устройства", "Сохранить", "Отмена");

        if (newDeviceDescription is null)
        {
            await DisplayAlert("Отмена", $"Отмена", "ОК");
            return;
        }

        // Добавление устройства и уведомление пользователя
        Devices.Add(new HomeDevice(newDeviceName, description: newDeviceDescription));

        await DisplayAlert(null, $"Устройство '{newDeviceName}' успешно добавлено", "ОК");
        
        */

        // Переход на следующую страницу - страницу нового устройства
        //  (и помещение её в стек навигации)
        await Shell.Current.Navigation.PushAsync(
            new DevicePage("Добавить устройство"));
    }


    /// <summary>
    /// Обработчих редактирования устройства
    /// </summary>
    private async void EditDevice(object sender, EventArgs e)
    {
        // проверяем, выбрал ли пользователь устройство из списка
        if (SelectedDevice is null)
        {
            await DisplayAlert(null, $"Пожалуйста, выберите устройство!", "OK");
            return;
        }

        // Переход на следующую страницу - страницу нового устройства
        //  (и помещение её в стек навигации)
        await Shell.Current.Navigation.PushAsync(
            new DevicePage("Изменить устройство", SelectedDevice));
    }


    /// <summary>
    /// Обработчик удаления устройства
    /// </summary>
    private async void RemoveDevice(object sender, EventArgs e)
    {
        /*

        // Получаем и "распаковываем" выбранный элемент
        var deviceToRemove = deviceList.SelectedItem as HomeDevice;
        if (deviceToRemove != null)
        {
            // Удаляем
            Devices.Remove(deviceToRemove);
            // Уведомляем пользователя
            await DisplayAlert(null, $"Устройство '{deviceToRemove.Name}' удалено", "ОК");
        }

        */

        // проверяем, выбрал ли пользователь устройство из списка
        if (SelectedDevice is null)
        {
            await DisplayAlert(null, $"Пожалуйста, выберите устройство!", "OK");
            return;
        }

        // Получаем сущность базы данных, которую следует удалить
        //  (мапим из внутренней сущности, представляющей выбранное устройство)
        var deviceToDelete = App.Mapper.Map<Data.HomeDeviceEntity>(SelectedDevice);
        // Удаляем сущность из бд
        await App.HomeDevices.DeleteHomeDevice(deviceToDelete);

        // Обновляем интерфейс
        var grp = DeviceGroups.FirstOrDefault(g => g.Name == SelectedDevice.Room);
        var deviceToRemove = grp?.FirstOrDefault(d => d.Id == deviceToDelete.Id);
        // if (deviceToRemove is not null)
        //     grp?.Remove(deviceToRemove);
        if (grp is not null)
            DeviceGroups.Remove(grp);
    }


    private async void OnLogout(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }


    private async void OnGoToProfile(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new ProfilePage());
    }
}
