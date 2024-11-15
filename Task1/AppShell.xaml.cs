namespace Task1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("about", typeof(AboutPage));
        }
    }
}
