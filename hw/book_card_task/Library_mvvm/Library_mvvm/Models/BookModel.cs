using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_mvvm.DAL;

namespace Library_mvvm.Models
{
    class BookModel
    {
        private readonly IBookProvider _bookProvider;
        private const string DefaultDataFilePath = "Books_List.xml";

        public BookModel(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        public Book[] Books { get; set; }
        public void LoadBooks()
        {
            LoadBooks(DefaultDataFilePath);
        }

        public void LoadBooks(string path)
        {
            Books = _bookProvider.LoadBooks(path);
        }

        public void Save()
        {
            _bookProvider.SaveBooks(DefaultDataFilePath, Books);
        }

        public void Save(string path)
        {
            _bookProvider.SaveBooks(path, Books);
        }
    }
}
