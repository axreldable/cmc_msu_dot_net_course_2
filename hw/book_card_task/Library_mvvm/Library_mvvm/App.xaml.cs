using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Library_mvvm.DAL;
using Library_mvvm.Models;
using Library_mvvm.ViewModels;

namespace Library_mvvm
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IBookProvider provider = new BookProvider();
            BookModel model = new BookModel(provider);
            BookViewModel viewModel = new BookViewModel(model);
            MainWindow window = new MainWindow { DataContext = viewModel };
        }
    }
}
