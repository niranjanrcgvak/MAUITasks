using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    [Table("books")]
    public class Book : BaseEntity
    {
        public string BookTitle { get; set; }
        public string Author { get; set; }
        [MinLength(13),Unique]
        public string ISBN { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
