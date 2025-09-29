namespace FirstMauiApp.Pages;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		if (count == 1)
			counterBtn.Text = $"Clicked {count} time";
		else
			counterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(counterBtn.Text);
	}
}
