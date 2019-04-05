using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library_mvvm.Annotations;

namespace Library_mvvm.DAL
{
    public class Book : INotifyPropertyChanged
    {
        private const string InitName = "Название книги";
        private const string InitAuthor = "Имя автора";
        private const Int32 InitYear = 2000;

        private string _name;
        private string _author;
        private int _year;

        public Book()
        {
            Name = InitName;
            Author = InitAuthor;
            Year = InitYear;
        }

        public Book(string name, string author, int year)
        {
            Name = name;
            Author = author;
            Year = year;
        }


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                RaisePropertyChanged();
            }
        }

        public int Year
        {
            get { return _year; }
            set
            {
                if (_year < 0) _year = 0;
                else _year = value;
                RaisePropertyChanged(nameof(Year));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
