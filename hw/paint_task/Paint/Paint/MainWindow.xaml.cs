using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint
{
    public partial class MainWindow : Window
    {
        enum Shape { Circle, Rectangle, Line, Triangle, Ellipse, RectangleCircle };

        private Shape CurrentShape { get; set; }
        List<Pair> Coordinates = new List<Pair>();

        public MainWindow()
        {
            InitializeComponent();

            SolidColorBrush[] brushes = new SolidColorBrush[]
            {
                Brushes.Red,
                Brushes.Coral,
                Brushes.Gold,
                Brushes.Green,
                Brushes.Blue,
                Brushes.DarkBlue,
                Brushes.Black,
                Brushes.Brown
            };

            FillColors.ItemsSource = brushes;
            BorderColors.ItemsSource = brushes;
        }

        private void Circle_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentShape = Shape.Circle;
        }

        private void Rectangle_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentShape = Shape.Rectangle;
        }

        private void RectangleCircle_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentShape = Shape.RectangleCircle;
        }

        private void Line_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentShape = Shape.Line;
        }

        private void Triangle_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentShape = Shape.Triangle;
        }

        private void Ellipse_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentShape = Shape.Ellipse;
        }

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            double x = e.GetPosition(Canvas).X;
            double y = e.GetPosition(Canvas).Y;
            Coordinates.Add(new Pair(x, y));

            TryToDraw();
        }

        private void TryToDraw()
        {
            if (CurrentShape != Shape.Triangle && Coordinates.Count == 2)
            {
                DrawShape();
            }
            else if (Coordinates.Count == 3)
            {
                DrawRectangle();
            }
            else
            {
                throw new Exception("Internal error type1!");
            }
        }

        private void DrawRectangle()
        {
            throw new NotImplementedException();
        }

        private void DrawShape()
        {
            switch (CurrentShape)
            {
                case Shape.Circle:
                {
                    break;
                }
                case Shape.Ellipse:
                {
                    break;
                }
                case Shape.Line:
                {
                    DrawLine(
                        Coordinates[0].X,
                        Coordinates[0].Y,
                        Coordinates[1].X,
                        Coordinates[1].Y
                        );
                    break;
                }
                case Shape.Rectangle:
                {
                    break;
                }
                case Shape.RectangleCircle:
                {
                    break;
                }
                case Shape.Triangle:
                {
                    break;
                }
            }
            Coordinates.Clear();
        }

        private void DrawLine(double x1, double y1, double x2, double y2)
        {
            Line line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Fill = (SolidColorBrush)FillColors.SelectedItem,
                StrokeThickness = 2,
                Stroke = (SolidColorBrush)BorderColors.SelectedItem
            };
            Canvas.Children.Add(line);
        }
    }

    internal class Pair
    {
        public double X;
        public double Y;

        public Pair(double x, double y) { X = x; Y = y; }
    }
}
