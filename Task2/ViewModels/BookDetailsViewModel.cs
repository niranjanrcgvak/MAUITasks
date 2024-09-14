using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Task2.Models;

namespace Task2.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class BookDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        Book book;

        [ObservableProperty]
        int id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
            Book = App.BookService.GetBook(Id);
        }
    }
}
