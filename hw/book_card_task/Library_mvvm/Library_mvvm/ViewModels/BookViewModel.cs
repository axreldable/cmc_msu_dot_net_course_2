using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Library_mvvm.Common;
using Library_mvvm.DAL;
using Library_mvvm.Models;

namespace Library_mvvm.ViewModels
{
    class BookViewModel : INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly BookModel _bookModel;
        private ObservableCollection<Book> _books;
        private Book _selectedBook;

        public BookViewModel(BookModel bookModel)
        {
            _bookModel = bookModel;

            LoadCommand = new RelayCommand(_ => Load());
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            AddCommand = new RelayCommand(_ => Add(), _ => Books != null);
            DeleteCommand = new RelayCommand(emp => Delete(emp as Book), emp => emp is Book);
            ExitCommand = new RelayCommand(_ => Application.Current.Shutdown());
        }

        public RelayCommand LoadCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand ExitCommand { get; }

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            private set
            {
                _books = value;
                RaisePropertyChanged();
            }
        }

        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                if (value != _selectedBook)
                {
                    _selectedBook = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void Load()
        {
            _bookModel.LoadBooks();
            Books = new ObservableCollection<Book>(_bookModel.Books);
        }

        public void Save()
        {
            _bookModel.Books = Books.ToArray();
            _bookModel.Save();
        }

        public bool CanSave()
        {
            return Books != null && Books.Count > 0;
        }

        public void Add()
        {
            Book book = new Book();
            Books.Add(book);
            SelectedBook = book;
        }

        private void Delete(Book book)
        {
            if (book == null) return;
            Books.Remove(book);
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
