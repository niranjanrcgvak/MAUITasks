namespace Task1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnNavigationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        private void OnUpdateClicked(object sender, EventArgs e)
        {
            string enteredText = inputField.Text;

            if (string.IsNullOrWhiteSpace(enteredText))
            {
                outputLabel.Text = "Please enter some text!";
                outputLabel.TextColor = Color.FromRgb(255, 0, 0);
            }
            else
            {
                outputLabel.Text = $"You entered: {enteredText}";
                outputLabel.TextColor = Color.FromRgb(0, 0, 0);
            }
        }
    }

}
