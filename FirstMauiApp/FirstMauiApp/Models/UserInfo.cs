//-
using System;
using System.ComponentModel;


namespace FirstMauiApp.Models;

public class UserInfo : INotifyPropertyChanged
{
    private string _name = null!;
    private string _email = null!;


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


    // Обновления этого свойства теперь получают все страницы
    public string Email
    {
        get { return _email; }

        set
        {
            if (_email != value)
            {
                _email = value;
                // Вызов уведомления при изменении
                OnPropertyChanged("Email");
            }
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
