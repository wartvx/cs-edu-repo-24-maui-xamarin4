namespace FirstMauiApp.Pages;

public partial class WebPage : ContentPage
{
    public WebPage()
    {
        InitializeComponent();

        InitializeWebView();
    }


    private void InitializeWebView()
    {
        // When a WebView is placed within a StackLayout
        //  (or VerticalStackLayout/HorizontalStackLayout),
        //  it requires explicit HeightRequest and WidthRequest properties to be set.
        //  Without these, the WebView will not render as it cannot determine
        //  its size within the layout

        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            webView.HeightRequest = 400;
            webView.WidthRequest = 600;
        }

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            webView.HeightRequest = 400;
            webView.WidthRequest = 200;
        }
    }


    private void OnWebViewNavigated(object sender, WebNavigatedEventArgs e)
    {
        // Do not use WebView.Source to get the current URL after navigation,
        //  as WebView.Source will always reflect the initial URL set
        //  for the WebView, not the URL of the page currently displayed
        //  after user navigation. The Navigated event and WebNavigatedEventArgs.Url
        //  are specifically designed to provide the URL of the page
        //  that has just finished loading within the WebView

        string currentUrl = e.Url;
        // You can now use 'currentUrl' as needed, e.g., display it in a Label

        urlEntry.Text = currentUrl;
    }


    /// <summary>
    /// Обработчик навигации
    /// </summary>
    private void NavigateToPage(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(urlEntry.Text))
            return;

        // переход по ссылке с автодополнением при необходимости
        webView.Source = new UrlWebViewSource
        {
            Url = urlEntry.Text.Contains("http") ? urlEntry.Text : $"https://{urlEntry.Text}"
        };
    }
}
