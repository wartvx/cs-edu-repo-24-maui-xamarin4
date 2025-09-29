//-
using System;
using System.Threading.Tasks;
using SQLite;


namespace FirstMauiApp.Data;

public class HomeDeviceRepository
{
    // Асинхронное подключение к Базе данных
    SQLiteAsyncConnection dbConnection;


    public HomeDeviceRepository(string databasePath)
    {
        // Создаем подключение в методе-конструкторе
        dbConnection = new SQLiteAsyncConnection(databasePath);
    }


    /// <summary>
    /// Проверяем на наличие таблицы и создаем в случае необходимости.
    /// </summary>
    public async Task InitDatabase()
    {
        await dbConnection.CreateTableAsync<HomeDeviceEntity>();
    }


    /// <summary>
    /// Получение всех устройств
    /// </summary>
    public async Task<HomeDeviceEntity[]> GetHomeDevices()
    {
        return await dbConnection.Table<HomeDeviceEntity>().ToArrayAsync();
    }


    /// <summary>
    /// Поиск устройства по идентификатору
    /// </summary>
    public async Task<HomeDeviceEntity> GetHomeDevice(int id)
    {
        return await dbConnection.GetAsync<HomeDeviceEntity>(id);
    }


    /// <summary>
    /// Удаление устройства
    /// </summary>
    public async Task<int> DeleteHomeDevice(HomeDeviceEntity homeDevice)
    {
        return await dbConnection.DeleteAsync(homeDevice);
    }


    /// <summary>
    /// Добавление устройства
    /// </summary>
    public async Task<int> AddHomeDevice(HomeDeviceEntity homeDevice)
    {
        return await dbConnection.InsertAsync(homeDevice);
    }


    /// <summary>
    /// Обновление существующего устройства
    /// </summary>
    public async Task<int> UpdateHomeDevice(HomeDeviceEntity homeDevice)
    {
        return await dbConnection.UpdateAsync(homeDevice);
    }
}
