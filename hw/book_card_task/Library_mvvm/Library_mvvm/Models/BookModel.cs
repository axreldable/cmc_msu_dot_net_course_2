using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_mvvm.DAL;

namespace Library_mvvm.Models
{
    class BookModel
    {
        private readonly IBookProvider _bookProvider;

        private const string FileName = "Books_List.xml";
        private readonly string _defaultDataFilePath = Path.Combine(Environment.CurrentDirectory, FileName);

        public BookModel(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        public Book[] Books { get; set; }
        public void LoadBooks()
        {
            LoadBooks(_defaultDataFilePath);
        }

        public void LoadBooks(string path)
        {
            Books = _bookProvider.LoadBooks(path);
        }

        public void Save()
        {
            _bookProvider.SaveBooks(_defaultDataFilePath, Books);
        }

        public void Save(string path)
        {
            _bookProvider.SaveBooks(path, Books);
        }
    }
}
