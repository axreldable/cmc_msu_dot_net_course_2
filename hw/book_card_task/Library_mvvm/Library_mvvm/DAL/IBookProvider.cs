using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_mvvm.DAL
{
    interface IBookProvider
    {
        Book[] LoadBooks(string path);
        void SaveBooks(string path, Book[] employees);
    }
}
