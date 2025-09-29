//-
using System;
using System.IO;
using AutoMapper;

using FirstMauiApp.Data;
using FirstMauiApp.Models;


namespace FirstMauiApp;

public partial class App : Application
{
    // Инициализация репозитория
    public static HomeDeviceRepository HomeDevices = new HomeDeviceRepository(
        Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData), $"homedevices.db"));

    // маппер
    public static IMapper Mapper { get; set; } = null!;


    public App()
    {
        Mapper = CreateMapper();

        // инициализация интерфейса
        InitializeComponent();
    }


    protected override Window CreateWindow(IActivationState? activationState)
    {
        // return new Window(new AppShell());

        var wins = new Window(new AppShell());

#if WINDOWS
        const int newHeight = 600;
        const int newWidth = 800;

        wins.Height = wins.MinimumHeight = wins.MaximumHeight = newHeight;
        wins.Width = wins.MinimumWidth = wins.MaximumWidth = newWidth;
#endif

        return wins;
    }


    /// <summary>
    /// Создание Автомаппера для преобразования сущностей
    /// </summary>
    public static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Data.HomeDeviceEntity, Models.HomeDevice>();
            cfg.CreateMap<Models.HomeDevice, Data.HomeDeviceEntity>();
        });

        return config.CreateMapper();
    }

    protected async override void OnStart()
    {
        await HomeDevices.InitDatabase();
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
}
