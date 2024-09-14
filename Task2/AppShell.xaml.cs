using Task2.Views;

namespace Task2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(BookDetailsPage), typeof(BookDetailsPage));
        }
    }
}
