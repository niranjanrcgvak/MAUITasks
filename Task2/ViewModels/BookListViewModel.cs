using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Task2.Models;
using Task2.Views;

namespace Task2.ViewModels
{
    public partial class BookListViewModel : BaseViewModel
    {
        const string editButtonText = "Update Book";
        const string createButtonText = "Add Book";
        public ObservableCollection<Book> Books { get; private set; } = new();

        public BookListViewModel()
        {
            Title = "Book List";
            AddEditButtonText = createButtonText;
            IsFormVisible = false;
            IsGridVisible = true;
            GetBookList().Wait();
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string bookTitle;
        [ObservableProperty]
        string author;
        [ObservableProperty]
        string isbn;
        [ObservableProperty]
        decimal? price;
        [ObservableProperty]
        int? stock;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        int bookId;
        [ObservableProperty]
        bool isFormVisible;
        [ObservableProperty]
        bool isGridVisible;

        [RelayCommand]
        async Task GetBookList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Books.Any()) Books.Clear();

                var books = App.BookService.GetBooks();
                foreach (var book in books)
                {
                    Books.Add(book);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get books: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrive list of books.", "Ok");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetBookDetails(int id)
        {
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}?Id={id}", true);
        }

        [RelayCommand]
        async Task SetEditMode(int id)
        {
            FormVisibilityTrue();
            Title = "Update Book";
            AddEditButtonText = editButtonText;
            BookId = id;
            var book = App.BookService.GetBook(id);
            BookTitle = book.BookTitle;
            Author = book.Author;
            Isbn = book.ISBN;
            Price = book.Price;
            Stock = book.Stock;
        }

        [RelayCommand]
        async Task SaveBook()
        {
            if (string.IsNullOrEmpty(BookTitle) || string.IsNullOrEmpty(Author) || string.IsNullOrEmpty(Isbn)|| Price <=0 || Stock <=0)
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
                return;
            }
            if(Isbn.Length < 13)
            {
                await Shell.Current.DisplayAlert("Invalid Data", "ISBN Value should be of 13 characters", "Ok");
                return;
            }

            var book = new Book
            {
                BookTitle = BookTitle,
                Author = Author,
                ISBN = Isbn,
                Price = Price,
                Stock = Stock,
            };

            if (BookId != 0)
            {
                book.Id = BookId;
                App.BookService.UpdateBook(book);
                await Shell.Current.DisplayAlert("Info", App.BookService.StatusMessage, "Ok");
            }
            else
            {
                App.BookService.AddBook(book);
                await Shell.Current.DisplayAlert("Info", App.BookService.StatusMessage, "Ok");
            }

            await GetBookList();
            await GridVisibilityTrue();
        }

        [RelayCommand]
        async Task DeleteBook(int id)
        {
            if (id == 0)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
                return;
            }
            var result = App.BookService.DeleteBook(id);
            if (result == 0)
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please try again", "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Deletion Successful", "Record Removed Successfully", "Ok");
                await GetBookList();
            }
        }

        [RelayCommand]
        async Task ClearForm()
        {
            if(BookId != 0)
            {
                AddEditButtonText = editButtonText;
            }
            else
            {
                AddEditButtonText = createButtonText;
                BookId = 0;
            }
            BookTitle = string.Empty;
            Author = string.Empty;
            Isbn = string.Empty;
            Price = null;
            Stock = null;
        }

        [RelayCommand]
        async Task FormVisibilityTrue()
        {
            Title = "Add Book";
            IsFormVisible = true;
            IsGridVisible = false;
        }

        [RelayCommand]
        async Task GridVisibilityTrue()
        {
            BookId = 0;
            await ClearForm();
            Title = "Book List";
            IsFormVisible = false;
            IsGridVisible = true;
        }
    }
}
