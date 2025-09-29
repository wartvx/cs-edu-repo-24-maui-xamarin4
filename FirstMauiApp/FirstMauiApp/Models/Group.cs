//-
using System;
using System.Collections.ObjectModel;


namespace FirstMauiApp.Models;

/// <summary>
/// Группируемая универсальная коллекция
/// </summary>
public class Group<K, T> : ObservableCollection<T>
{
    public K Name { get; private set; }
    
    public Group(K name, IEnumerable<T> items)
    {
        Name = name;
        foreach (T item in items)
            Items.Add(item);
    }
}
