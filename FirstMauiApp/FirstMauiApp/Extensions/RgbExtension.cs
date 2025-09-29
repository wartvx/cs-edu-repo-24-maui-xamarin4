//-
using System;


namespace FirstMauiApp.Extensions;

public class RgbExtension : IMarkupExtension
{
    public int Red { get; set; }
    public int Green { get; set; }
    public int Blue { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return Color.FromRgb(Red, Green, Blue);
    }
}
