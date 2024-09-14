using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.Services
{
    public class BookService
    {
        SQLiteConnection conn;
        string _dbPath;
        public string StatusMessage;
        int result = 0;

        public BookService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (conn != null)
            {
                return;
            }
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Book>();
        }

        public List<Book> GetBooks()
        {
            try
            {
                Init();
                return conn.Table<Book>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data";
            }
            return new List<Book>();
        }

        public Book GetBook(int id)
        {
            try
            {
                Init();
                return conn.Table<Book>().FirstOrDefault(q => q.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data";
            }
            return null;
        }

        public void AddBook(Book book)
        {
            try
            {
                Init();
                if (book == null)
                {
                    throw new Exception("Invalid Book Record");
                }

                result = conn.Insert(book);
                StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to Insert data";
            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                Init();

                if (book == null)
                    throw new Exception("Invalid Book Record");

                result = conn.Update(book);
                StatusMessage = result == 0 ? "Update Failed" : "Update Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to Update data.";
            }
        }

        public int DeleteBook(int id)
        {
            try
            {
                Init();
                return conn.Table<Book>().Delete(q => q.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete data";
            }
            return 0;
        }
    }
}
