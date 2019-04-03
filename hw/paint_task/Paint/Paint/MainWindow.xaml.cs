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

        private const Int32 ConstStrokeThickness = 2;
        private const double ConstRectangleRadius = 15;

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

            FillColors.SelectedIndex = 0;
            BorderColors.SelectedIndex = 0;
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
                Coordinates.Clear();
            }
            else if (Coordinates.Count == 3)
            {
                DrawTriangle(
                    Coordinates[0].X, Coordinates[0].Y,
                    Coordinates[1].X, Coordinates[1].Y,
                    Coordinates[2].X, Coordinates[2].Y);
                Coordinates.Clear();
            }
        }

        private void DrawTriangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            Polygon triangle = new Polygon()
            {
                Fill = (SolidColorBrush) FillColors.SelectedItem,
                Stroke = (SolidColorBrush) BorderColors.SelectedItem,
                StrokeThickness = ConstStrokeThickness
            };
            triangle.Points.Add(new Point(x1, y1));
            triangle.Points.Add(new Point(x2, y2));
            triangle.Points.Add(new Point(x3, y3));

            Canvas.Children.Add(triangle);
        }

        private void DrawShape()
        {
            switch (CurrentShape)
            {
                case Shape.Circle:
                    {
                        DrawCircle(Coordinates[0].X, Coordinates[0].Y, Coordinates[1].X, Coordinates[1].Y);
                        break;
                    }
                case Shape.Ellipse:
                    {
                        DrawEllipse(Coordinates[0].X, Coordinates[0].Y, Coordinates[1].X, Coordinates[1].Y);
                        break;
                    }
                case Shape.Line:
                    {
                        DrawLine(Coordinates[0].X, Coordinates[0].Y, Coordinates[1].X, Coordinates[1].Y);
                        break;
                    }
                case Shape.Rectangle:
                    {
                        DrawRectangle(Coordinates[0].X, Coordinates[0].Y, Coordinates[1].X, Coordinates[1].Y);
                        break;
                    }
                case Shape.RectangleCircle:
                    {
                        DrawRectangleCircle(Coordinates[0].X, Coordinates[0].Y, Coordinates[1].X, Coordinates[1].Y);
                        break;
                    }
                case Shape.Triangle:
                    {
                        break;
                    }
            }
        }

        private void DrawRectangleCircle(double x1, double y1, double x2, double y2)
        {
            Rectangle rectangle = new Rectangle()
            {
                Height = Math.Abs(y1 - y2),
                Width = Math.Abs(x1 - x2),
                RadiusX = ConstRectangleRadius,
                RadiusY = ConstRectangleRadius,
                Fill = (SolidColorBrush)FillColors.SelectedItem,
                StrokeThickness = ConstStrokeThickness,
                Stroke = (SolidColorBrush)BorderColors.SelectedItem
            };
            Canvas.SetLeft(rectangle, Math.Min(x1, x2));
            Canvas.SetTop(rectangle, Math.Min(y1, y2));
            Canvas.Children.Add(rectangle);
        }

        private void DrawEllipse(double x1, double y1, double x2, double y2)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = Math.Abs(x1 - x2),
                Height = Math.Abs(y1 - y2),
                Fill = (SolidColorBrush)FillColors.SelectedItem,
                Stroke = (SolidColorBrush)BorderColors.SelectedItem,
                StrokeThickness = ConstStrokeThickness
            };
            Canvas.SetLeft(ellipse, Math.Min(x1, x2));
            Canvas.SetTop(ellipse, Math.Min(y1, y2));
            Canvas.Children.Add(ellipse);
        }

        private void DrawCircle(double x1, double y1, double x2, double y2)
        {
            Ellipse circle = new Ellipse
            {
                Width = Math.Abs(x1 - x2),
                Height = Math.Abs(x1 - x2),
                Fill = (SolidColorBrush)FillColors.SelectedItem,
                Stroke = (SolidColorBrush)BorderColors.SelectedItem,
                StrokeThickness = ConstStrokeThickness
            };
            Canvas.SetLeft(circle, Math.Min(x1, x2));
            Canvas.SetTop(circle, Math.Min(y1, y2));
            Canvas.Children.Add(circle);
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
                StrokeThickness = ConstStrokeThickness,
                Stroke = (SolidColorBrush)BorderColors.SelectedItem
            };
            Canvas.Children.Add(line);
        }

        private void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            Rectangle rectangle = new Rectangle()
            {
                Height = Math.Abs(y1 - y2),
                Width = Math.Abs(x1 - x2),
                Fill = (SolidColorBrush)FillColors.SelectedItem,
                StrokeThickness = ConstStrokeThickness,
                Stroke = (SolidColorBrush)BorderColors.SelectedItem
            };
            Canvas.SetLeft(rectangle, Math.Min(x1, x2));
            Canvas.SetTop(rectangle, Math.Min(y1, y2));
            Canvas.Children.Add(rectangle);
        }
    }

    internal class Pair
    {
        public double X;
        public double Y;

        public Pair(double x, double y) { X = x; Y = y; }
    }
}
