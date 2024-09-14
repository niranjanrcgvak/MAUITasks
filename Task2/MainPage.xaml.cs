using Task2.ViewModels;

namespace Task2
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(BookListViewModel bookListViewModel)
        {
            InitializeComponent();
            BindingContext = bookListViewModel;
        }
    }

}
