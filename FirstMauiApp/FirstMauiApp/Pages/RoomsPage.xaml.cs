namespace FirstMauiApp.Pages;

public partial class RoomsPage : ContentPage
{
	// константа для текста кнопки
	public const string BUTTON_TEXT = "Добавить комнату";

	int count = 0;

	public RoomsPage()
	{
		InitializeComponent();
	}

	private void OnAddClicked(object? sender, EventArgs e)
	{
		count++;

	}
}