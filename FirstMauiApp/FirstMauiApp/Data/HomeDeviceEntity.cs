//-
using System;
using SQLite;


namespace FirstMauiApp.Data;

/// <summary>
/// Класс - модель таблицы устройств
/// </summary>
[Table("HomeDevices")]
public class HomeDeviceEntity
{
    [PrimaryKey]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Room { get; set; }
}
