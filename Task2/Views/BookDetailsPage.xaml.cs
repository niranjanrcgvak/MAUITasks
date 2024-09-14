using Task2.ViewModels;

namespace Task2.Views;

public partial class BookDetailsPage : ContentPage
{
	public BookDetailsPage(BookDetailsViewModel bookDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = bookDetailsViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}