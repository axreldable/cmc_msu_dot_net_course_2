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
        private Author _selectedAuthor;

        public BookViewModel(BookModel bookModel)
        {
            _bookModel = bookModel;

            LoadCommand = new RelayCommand(_ => Load());
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            AddCommand = new RelayCommand(_ => Add());
            DeleteCommand = new RelayCommand(emp => Delete(emp as Book), emp => emp is Book);
            ExitCommand = new RelayCommand(_ => Application.Current.Shutdown());
            AddAuthorCommand = new RelayCommand(o => AuthorAdd(), o => CanAuthorAdd());
            DeleteAuthorCommand = new RelayCommand(SelectedAuthorDelete, CanAuthorDelete);
        }

        public RelayCommand LoadCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand AddAuthorCommand { get; }
        public RelayCommand DeleteAuthorCommand { get; }

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
   
        public Author SelectedAuthor
        {
            get { return _selectedAuthor; }
            set
            {
                if (_selectedAuthor != value)
                {
                    _selectedAuthor = value;
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
            if (Books == null)
            {
                Books = new ObservableCollection<Book>();
            }
            Book book = new Book();
            Books.Add(book);
            SelectedBook = book;
        }

        private void Delete(Book book)
        {
            if (book == null) return;
            Books.Remove(book);
        }

        private bool CanAuthorAdd()
        {
            return SelectedBook != null;
        }

        private void AuthorAdd()
        {
            Author author = new Author();
            SelectedBook.AddAuthor(author);
            SelectedAuthor = author;
        }

        private bool CanAuthorDelete(object selectedItem)
        {
            return selectedItem != null && selectedItem is Author;
        }

        private void SelectedAuthorDelete(object selectedItem)
        {
            SelectedBook.DellAuthor((Author)selectedItem);
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
