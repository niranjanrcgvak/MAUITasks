using Microsoft.Extensions.Logging;
using Task2.Services;
using Task2.ViewModels;
using Task2.Views;

namespace Task2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "books.db3");

            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<BookService>(s, dbPath));

            builder.Services.AddSingleton<BookListViewModel>();
            builder.Services.AddSingleton<BookDetailsViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<BookDetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}
