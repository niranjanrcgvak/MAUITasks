namespace Task1;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}
    private async void OnNavigationClicked(object sender, EventArgs e)
    {
        // Navigate to the SecondPage
        await Navigation.PushAsync(new MainPage());
    }
}