using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Notepad
{
    public partial class MainWindow : Window
    {
        private const string DialogTextFile = "Текстовый файл";
        private const string InitialFileName = "Новый текстовый документ";
        private const string NewFileQuestion = "Хотите сохранить изменения перед созданием нового файла?";
        private const string OpenFileQuestion = "Хотите сохранить изменения перед открытием нового файла?";
        private const string ExitQuestion = "Хотите сохранить изменения перед выходом?";

        private bool _isTextChanged = false;
        private string CurrentFilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isTextChanged = true;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (_isTextChanged)
            {
                MessageBoxResult result = MessageBox.Show(
                    NewFileQuestion, 
                    "", 
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (SaveAs_Command() == true)
                        {
                            ClearTextField();
                        }
                        break;
                    case MessageBoxResult.No:
                        ClearTextField();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                ClearTextField();
            }
        }

        private void ClearTextField()
        {
            CurrentFilePath = null;
            _isTextChanged = false;

            TextEditor.Text = "";
            _isTextChanged = false;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (_isTextChanged)
            {
                MessageBoxResult result = MessageBox.Show(
                    OpenFileQuestion,
                    "",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (SaveAs_Command() == true)
                        {
                            OpenFile();
                        }
                        break;
                    case MessageBoxResult.No:
                        OpenFile();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                OpenFile();
            }

        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = $"{DialogTextFile}|*.txt",
            };

            if (openFileDialog.ShowDialog() == true)
            {
                CurrentFilePath = openFileDialog.FileName;
                TextEditor.Text = File.ReadAllText(openFileDialog.FileName);
                _isTextChanged = false;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentFilePath == null)
            {
                SaveAs_Command();
            }
            else
            {
                File.WriteAllText(CurrentFilePath, TextEditor.Text);
                _isTextChanged = false;
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveAs_Command();
        }

        private bool SaveAs_Command()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = $"{DialogTextFile}|*.txt",
                FileName = CurrentFileName()
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, TextEditor.Text);
                _isTextChanged = false;
                return true;
            }
            return false;
        }

        private void Wrapping_OnChecked(object sender, RoutedEventArgs e)
        {
            TextEditor.TextWrapping = TextWrapping.Wrap;
        }

        private void Wrapping_OnUnchecked(object sender, RoutedEventArgs e)
        {
            TextEditor.TextWrapping = TextWrapping.NoWrap;
        }

        private void SpellCheck_OnChecked(object sender, RoutedEventArgs e)
        {
            TextEditor.SpellCheck.IsEnabled = true;
        }

        private void SpellCheck_OnUnchecked(object sender, RoutedEventArgs e)
        {
            TextEditor.SpellCheck.IsEnabled = false;
        }

        private string CurrentFileName()
        {
            if (CurrentFilePath == null)
            {
                return InitialFileName;
            }

            return Path.GetFileName(CurrentFilePath);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_isTextChanged)
            {
                MessageBoxResult result = MessageBox.Show(
                    ExitQuestion,
                    "",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (SaveAs_Command() == true)
                        {
                            Application.Current.Shutdown();
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                        break;
                    case MessageBoxResult.No:
                        Application.Current.Shutdown();
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
