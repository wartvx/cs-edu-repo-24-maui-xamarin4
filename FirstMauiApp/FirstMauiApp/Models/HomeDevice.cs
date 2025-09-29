//-
using System;
using System.ComponentModel;


namespace FirstMauiApp.Models;

public class HomeDevice : INotifyPropertyChanged
{
    private string _name = null!;
    private string? _description;


    public Guid Id { get; set; }

    // public string Name { get; set; }
    // public string? Description { get; set; }

    public string? Image { get; set; }
    public string? Room { get; set; }


    // Обновления этого свойства теперь получают все страницы
    public string Name
    {
        get { return _name; }

        set
        {
            if (_name != value)
            {
                _name = value;
                // Вызов уведомления при изменении
                OnPropertyChanged("Name");
            }
        }
    }

    // Обновления этого свойства теперь получают все страинцы
    public string? Description
    {
        get { return _description; }

        set
        {
            if (_description != value)
            {
                _description = value;
                // Вызов уведомления при изменении
                OnPropertyChanged("Description");
            }
        }
    }


    public HomeDevice(string name, string? image = null,
        string? description = null, string? room = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Image = image;
        Description = description;
        Room = room;
    }


    // Реализация интерфейса INotifyPropertyChanged
    //  для автоматического обновления интерфейса во всех страницах
    //  при обновлении модели

    /// <summary>
    /// Делегат, указывающий на метод-обработчик события PropertyChanged,
    ///  возникающего при изменении свойств компонента
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Метод, вызывающий событие PropertyChanged
    /// </summary>
    public void OnPropertyChanged(string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
