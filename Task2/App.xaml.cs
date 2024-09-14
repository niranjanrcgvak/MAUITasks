using Task2.Services;

namespace Task2
{
    public partial class App : Application
    {
        public static BookService BookService { get; set; }
        public App(BookService bookService)
        {
            InitializeComponent();

            MainPage = new AppShell();
            BookService = bookService;
        }
    }
}
