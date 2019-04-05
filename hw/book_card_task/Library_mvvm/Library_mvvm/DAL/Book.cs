using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Author> _authors;
        private int _year;

        public Book()
        {
            Name = InitName;
            Authors = new ObservableCollection<Author>();
            Year = InitYear;
        }

        public Book(string name, string author, int year)
        {
            Name = name;
            Authors = new ObservableCollection<Author>()
            {
                new Author()
            };
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

        public ObservableCollection<Author> Authors
        {
            get { return _authors; }
            set
            {
                if (_authors != value)
                {
                    _authors = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void AddAuthor(Author nameAuthor)
        {
            if (Authors == null)
            {
                Authors = new ObservableCollection<Author>();
            }
            Authors.Add(nameAuthor);
            RaisePropertyChanged();
        }

        public void DellAuthor(Author nameAuthor)
        {
            Authors.Remove(nameAuthor);
            RaisePropertyChanged();
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
