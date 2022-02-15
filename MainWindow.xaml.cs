using System;
using System.Collections.Generic;
using System.Linq;
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

namespace GeomAns
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string theme = "light";
        public bool drawing = false;
        public Color col = Colors.Black;
        private Point prev;
        private bool isPaint = false;
        private static int SIZE = 2;
        private int SHIFT = SIZE / 2;
        private int points_left = 0;
        private double previous_X = 0;
        private double previous_Y = 0;
        private double double_previous_X = 0;
        private double double_previous_Y = 0;
        private bool ray = false;
        private bool straight = false;
        private int counter = -1;
        private int counter1 = 0;
        private int drawing_now = 0;
        private string[] dots = new string[1000];
        private double[] lenghths = new double[1000];
        public MainWindow()
        {
            InitializeComponent();
            this.Working_area.Background = new SolidColorBrush(Colors.LightGray);
            this.Menu1.Background = new SolidColorBrush(Colors.LightGray);
            this.Menu2.Background = new SolidColorBrush(Colors.LightGray);
        }
        private void Settings_call(object sender, RoutedEventArgs e)
        {
            Window1 s = new Window1
            {
                Owner = this
            };
            if (theme == "dark")
            {
                s.Dark_theme.IsChecked = true;
            }
            s.Drawing_size.Value = SIZE;
            s.ShowDialog();
            if(s.theme == "light")
            {
                this.Background = new SolidColorBrush(Colors.White);
                this.Working_area.Background = new SolidColorBrush(Colors.LightGray);
                this.Menu1.Background = new SolidColorBrush(Colors.LightGray);
                this.Menu2.Background = new SolidColorBrush(Colors.LightGray);
                this.theme = "light";
            }
            else
            {
                this.Background = new SolidColorBrush(Colors.DimGray);
                this.Working_area.Background = new SolidColorBrush(Colors.Gray);
                this.Menu1.Background = new SolidColorBrush(Colors.Gray);
                this.Menu2.Background = new SolidColorBrush(Colors.Gray);
                this.theme = "dark";
            }
            SIZE = (int)s.Drawing_size.Value;
        }

        private void Exit_call(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Working_area_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                if (!isPaint) return;
                var point = Mouse.GetPosition(Working_area);
                var line = new Line
                {
                    Stroke = new SolidColorBrush(col),
                    StrokeThickness = SIZE,
                    X1 = prev.X,
                    Y1 = prev.Y,
                    X2 = point.X,
                    Y2 = point.Y,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };
                prev = point;
                Working_area.Children.Add(line);
            }
        }

        private void Working_area_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (drawing)
            {
                if (isPaint) return;
                isPaint = true;
                prev = Mouse.GetPosition(Working_area);
                var dot = new Ellipse { Width = SIZE, Height = SIZE, Fill = new SolidColorBrush(col) };
                dot.SetValue(Canvas.LeftProperty, prev.X - SHIFT);
                dot.SetValue(Canvas.TopProperty, prev.Y - SHIFT);
                Working_area.Children.Add(dot);
            }
            else if (drawing_now == 1 && points_left == 1)
            {
                prev = Mouse.GetPosition(Working_area);
                var dot = new Ellipse { Width = 8, Height = 8, Fill = new SolidColorBrush(col) };
                dot.SetValue(Canvas.LeftProperty, prev.X - SHIFT);
                dot.SetValue(Canvas.TopProperty, prev.Y - SHIFT);
                Working_area.Children.Add(dot);
                points_left--;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = dots[counter];
                textBlock.Foreground = new SolidColorBrush(col);
                Canvas.SetLeft(textBlock, prev.X + 10);
                Canvas.SetTop(textBlock, prev.Y + 10);
                Working_area.Children.Add(textBlock);
            }
            else if (drawing_now == 2 && points_left == 2)
            {
                prev = Mouse.GetPosition(Working_area);
                var dot1 = new Ellipse { Width = 8, Height = 8, Fill = new SolidColorBrush(col) };
                dot1.SetValue(Canvas.LeftProperty, prev.X - SHIFT);
                dot1.SetValue(Canvas.TopProperty, prev.Y - SHIFT);
                Working_area.Children.Add(dot1);
                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = dots[counter - 1];
                textBlock1.Foreground = new SolidColorBrush(col);
                Canvas.SetLeft(textBlock1, prev.X + 10);
                Canvas.SetTop(textBlock1, prev.Y + 10);
                Working_area.Children.Add(textBlock1);
                points_left--;
                previous_X = prev.X;
                previous_Y = prev.Y;
            }
            else if (drawing_now == 2 && points_left == 1)
            {
                prev = Mouse.GetPosition(Working_area);
                var dot2 = new Ellipse { Width = 8, Height = 8, Fill = new SolidColorBrush(col) };
                dot2.SetValue(Canvas.LeftProperty, prev.X - SHIFT);
                dot2.SetValue(Canvas.TopProperty, prev.Y - SHIFT);
                Working_area.Children.Add(dot2);
                TextBlock textBlock2 = new TextBlock();
                textBlock2.Text = dots[counter];
                textBlock2.Foreground = new SolidColorBrush(col);
                Canvas.SetLeft(textBlock2, prev.X + 10);
                Canvas.SetTop(textBlock2, prev.Y + 10);
                Working_area.Children.Add(textBlock2);
                points_left--;
                drawing_now = 0;
                double X1 = 0;
                double Y1 = 0;
                double X2 = 0;
                double Y2 = 0;
                if (ray)
                {
                    if(previous_X == prev.X)
                    {
                        X1 = previous_X;
                        X2 = previous_X;
                        Y1 = previous_Y;
                        if (Y1 < Y2)
                        {
                            Y2 = 0;
                        }
                        else
                        {
                            Y2 = Working_area.Height;
                        }
                    }
                    else if(previous_Y == prev.Y)
                    {
                        Y1 = previous_Y;
                        Y2 = previous_Y;
                        X1 = previous_X;
                        if (X1 < X2)
                        {
                            X2 = 0;
                        }
                        else
                        {
                            X2 = Working_area.Width;
                        }
                    }
                    else
                    {
                        double k = (prev.Y - previous_Y) / (prev.X - previous_X);
                        double b = prev.Y - k * prev.X;
                        X1 = previous_X;
                        X2 = previous_Y;
                        while (true)
                        {
                            if (b >= 0 && b <= Working_area.Height)
                            {
                                Y2 = b;
                                X2 = 0;
                                break;
                            }
                            if (k * Working_area.Width + b >= 0 && k * Working_area.Width + b <= Working_area.Height)
                            {
                                Y2 = k * Working_area.Width + b;
                                X2 = Working_area.Width;
                                break;
                            }
                            if (-b / k >= 0 && -b / k <= Working_area.Width)
                            {
                                Y2 = 0;
                                X2 = -b / k;
                                break;
                            }
                            if ((Working_area.Height - b) / k >= 0 && (Working_area.Height - b) / k <= Working_area.Width)
                            {
                                Y2 = Working_area.Height;
                                X2 = (Working_area.Height - b) / k;
                                break;
                            }
                        }
                        while (true)
                        {
                            if ((Working_area.Height - b) / k >= 0 && (Working_area.Height - b) / k <= Working_area.Width)
                            {
                                Y1 = Working_area.Height;
                                X1 = (Working_area.Height - b) / k;
                                break;
                            }
                            if (-b / k >= 0 && -b / k <= Working_area.Width)
                            {
                                Y1 = 0;
                                X1 = -b / k;
                                break;
                            }
                            if (k * Working_area.Width + b >= 0 && k * Working_area.Width + b <= Working_area.Height)
                            {
                                Y1 = k * Working_area.Width + b;
                                X1 = Working_area.Width;
                                break;
                            }
                            if (b >= 0 && b <= Working_area.Height)
                            {
                                Y1 = b;
                                X1 = 0;
                                break;
                            }
                        }
                        if(Math.Sqrt((prev.X - X1) * (prev.X - X1) + (prev.Y - Y1) * (prev.Y - Y1)) < Math.Sqrt((prev.X - X2) * (prev.X - X2) + (prev.Y - Y2) * (prev.Y - Y2)))
                        {
                            X2 = previous_X;
                            Y2 = previous_Y;
                        }
                        else
                        {
                            X1 = previous_X;
                            Y1 = previous_Y;
                        }
                    }
                    ray = false;
                }
                else if (straight)
                {
                    if (previous_X == prev.X)
                    {
                        X1 = previous_X;
                        X2 = previous_X;
                        if (Y1 < Y2)
                        {
                            Y2 = 0;
                            Y1 = Working_area.Height;
                        }
                        else
                        {
                            Y2 = Working_area.Height;
                            Y1 = 0;
                        }
                    }
                    else if (previous_Y == prev.Y)
                    {
                        Y1 = previous_Y;
                        Y2 = previous_Y;
                        if (X1 < X2)
                        {
                            X2 = 0;
                            X1 = Working_area.Width;
                        }
                        else
                        {
                            X2 = Working_area.Width;
                            X1 = 0;
                        }
                    }
                    else
                    {
                        double k = (prev.Y - previous_Y) / (prev.X - previous_X);
                        double b = prev.Y - k * prev.X;
                        while (true)
                        {
                            if(b >= 0 && b <= Working_area.Height)
                            {
                                Y2 = b;
                                X2 = 0;
                                break;
                            }
                            if(k * Working_area.Width + b >= 0 && k * Working_area.Width + b <= Working_area.Height)
                            {
                                Y2 = k * Working_area.Width + b;
                                X2 = Working_area.Width;
                                break;
                            }
                            if(-b / k >= 0 && -b / k <= Working_area.Width)
                            {
                                Y2 = 0;
                                X2 = -b / k;
                                break;
                            }
                            if((Working_area.Height - b) / k >= 0 && (Working_area.Height - b) / k <= Working_area.Width)
                            {
                                Y2 = Working_area.Height;
                                X2 = (Working_area.Height - b) / k;
                                break;
                            }
                        }
                        while (true)
                        {
                            if ((Working_area.Height - b) / k >= 0 && (Working_area.Height - b) / k <= Working_area.Width)
                            {
                                Y1 = Working_area.Height;
                                X1 = (Working_area.Height - b) / k;
                                break;
                            }
                            if (-b / k >= 0 && -b / k <= Working_area.Width)
                            {
                                Y1 = 0;
                                X1 = -b / k;
                                break;
                            }
                            if (k * Working_area.Width + b >= 0 && k * Working_area.Width + b <= Working_area.Height)
                            {
                                Y1 = k * Working_area.Width + b;
                                X1 = Working_area.Width;
                                break;
                            }
                            if (b >= 0 && b <= Working_area.Height)
                            {
                                Y1 = b;
                                X1 = 0;
                                break;
                            }
                        }
                    }
                    straight = false;
                }
                else
                {
                    X1 = previous_X;
                    X2 = prev.X;
                    Y1 = previous_Y;
                    Y2 = prev.Y;
                }
                var line = new Line
                {
                    Stroke = new SolidColorBrush(col),
                    StrokeThickness = SIZE,
                    X1 = X1,
                    Y1 = Y1,
                    X2 = X2,
                    Y2 = Y2,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };
                if (lenghths[counter1] > 0)
                {
                    TextBlock textBlock1 = new TextBlock();
                    textBlock1.Text = lenghths[counter1].ToString();
                    textBlock1.Foreground = new SolidColorBrush(col);
                    Canvas.SetLeft(textBlock1, (prev.X + previous_X) / 2 + 10);
                    Canvas.SetTop(textBlock1, (prev.Y + previous_Y) / 2 + 10);
                    Working_area.Children.Add(textBlock1);
                }
                else if(lenghths[counter1] < 0)
                {
                    TextBlock textBlock1 = new TextBlock();
                    textBlock1.Text = "?";
                    textBlock1.Foreground = new SolidColorBrush(col);
                    Canvas.SetLeft(textBlock1, (prev.X + previous_X) / 2 + 10);
                    Canvas.SetTop(textBlock1, (prev.Y + previous_Y) / 2 + 10);
                    Working_area.Children.Add(textBlock1);
                }
                
                Working_area.Children.Add(line);
            }
            else if(drawing_now == 3 && points_left == 3)
            {
                prev = Mouse.GetPosition(Working_area);
                var dot1 = new Ellipse { Width = 8, Height = 8, Fill = new SolidColorBrush(col) };
                dot1.SetValue(Canvas.LeftProperty, prev.X - SHIFT);
                dot1.SetValue(Canvas.TopProperty, prev.Y - SHIFT);
                Working_area.Children.Add(dot1);
                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = "A";
                textBlock1.Foreground = new SolidColorBrush(col);
                Canvas.SetLeft(textBlock1, prev.X + 10);
                Canvas.SetTop(textBlock1, prev.Y + 10);
                Working_area.Children.Add(textBlock1);
                points_left--;
                double_previous_X = prev.X;
                double_previous_Y = prev.Y;
            }
            else if (drawing_now == 3 && points_left == 2)
            {
                prev = Mouse.GetPosition(Working_area);
                var dot1 = new Ellipse { Width = 8, Height = 8, Fill = new SolidColorBrush(col) };
                dot1.SetValue(Canvas.LeftProperty, prev.X - SHIFT);
                dot1.SetValue(Canvas.TopProperty, prev.Y - SHIFT);
                Working_area.Children.Add(dot1);
                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = "B";
                textBlock1.Foreground = new SolidColorBrush(col);
                Canvas.SetLeft(textBlock1, prev.X + 10);
                Canvas.SetTop(textBlock1, prev.Y + 10);
                Working_area.Children.Add(textBlock1);
                points_left--;
                previous_X = prev.X;
                previous_Y = prev.Y;
                double X1 = double_previous_X;
                double Y1 = double_previous_Y;
                double X2 = prev.X;
                double Y2 = prev.Y;
                var line = new Line
                {
                    Stroke = new SolidColorBrush(col),
                    StrokeThickness = SIZE,
                    X1 = X1,
                    Y1 = Y1,
                    X2 = X2,
                    Y2 = Y2,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };
                TextBlock textBlock2 = new TextBlock();
                textBlock2.Text = "3";
                textBlock2.Foreground = new SolidColorBrush(col);
                Canvas.SetLeft(textBlock2, (X1 + X2) / 2 + 10);
                Canvas.SetTop(textBlock2, (Y1 + Y2) / 2 + 10);
                Working_area.Children.Add(textBlock2);
                Working_area.Children.Add(line);
            }
            else if (drawing_now == 3 && points_left == 1)
            {
                prev = Mouse.GetPosition(Working_area);
                var dot1 = new Ellipse { Width = 8, Height = 8, Fill = new SolidColorBrush(col) };
                dot1.SetValue(Canvas.LeftProperty, prev.X - SHIFT);
                dot1.SetValue(Canvas.TopProperty, prev.Y - SHIFT);
                Working_area.Children.Add(dot1);
                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = "C";
                textBlock1.Foreground = new SolidColorBrush(col);
                Canvas.SetLeft(textBlock1, prev.X + 10);
                Canvas.SetTop(textBlock1, prev.Y + 10);
                Working_area.Children.Add(textBlock1);
                points_left--;
                double X1 = double_previous_X;
                double Y1 = double_previous_Y;
                double X2 = prev.X;
                double Y2 = prev.Y;
                var line = new Line
                {
                    Stroke = new SolidColorBrush(col),
                    StrokeThickness = SIZE,
                    X1 = X1,
                    Y1 = Y1,
                    X2 = X2,
                    Y2 = Y2,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };
                TextBlock textBlock2 = new TextBlock();
                textBlock2.Text = "5";
                textBlock2.Foreground = new SolidColorBrush(col);
                Canvas.SetLeft(textBlock2, (X1 + X2) / 2 + 10);
                Canvas.SetTop(textBlock2, (Y1 + Y2) / 2 + 10);
                Working_area.Children.Add(textBlock2);
                Working_area.Children.Add(line);
                X1 = previous_X;
                Y1 = previous_Y;
                line = new Line
                {
                    Stroke = new SolidColorBrush(col),
                    StrokeThickness = SIZE,
                    X1 = X1,
                    Y1 = Y1,
                    X2 = X2,
                    Y2 = Y2,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };
                TextBlock textBlock3 = new TextBlock();
                textBlock3.Text = "?";
                textBlock3.Foreground = new SolidColorBrush(col);
                Canvas.SetLeft(textBlock3, (X1 + X2) / 2 + 10);
                Canvas.SetTop(textBlock3, (Y1 + Y2) / 2 + 10);
                Working_area.Children.Add(textBlock3);
                Working_area.Children.Add(line);
            }

        }

        private void Working_area_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (drawing)
            {
                isPaint = false;
            }
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            Window2 c = new Window2
            {
                Owner = this
            };
            c.ShowDialog();
            this.col = c.color;

    }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Working_area.Children.Clear();
        }

        private void Draw_Checked(object sender, RoutedEventArgs e)
        {
            drawing = true;
        }

        private void Draw_Unchecked(object sender, RoutedEventArgs e)
        {
            drawing = false;
        }

        private void Point_Click(object sender, RoutedEventArgs e)
        {
            drawing = false;
            points_left = 1;
            Draw.IsChecked = false;
            Window3 b = new Window3
            {
                Owner = this
            };
            b.Height = 80;
            b.P1.Content = "Point's name:";
            b.ShowDialog();
            counter++;
            dots[counter] = b.point_1_name;
            drawing_now = 1;
        }
        private void Line_segment_Click(object sender, RoutedEventArgs e)
        {
            drawing = false;
            points_left = 2;
            Draw.IsChecked = false;
            Window4 b = new Window4
            {
                Owner = this
            };
            b.Title += "line segment";
            b.ShowDialog();
            counter++;
            dots[counter] = b.point_1_name;
            counter++;
            dots[counter] = b.point_2_name;
            counter1++;
            lenghths[counter1] = b.length_value;
            drawing_now = 2;
        }

        private void Ray_Click(object sender, RoutedEventArgs e)
        {
            drawing = false;
            points_left = 2;
            Draw.IsChecked = false;
            Window4 b = new Window4
            {
                Owner = this
            };
            b.Title += "ray";
            b.ShowDialog();
            counter++;
            dots[counter] = b.point_1_name;
            counter++;
            dots[counter] = b.point_2_name;
            counter1++;
            lenghths[counter1] = b.length_value;
            drawing_now = 2;
            ray = true;
        }

        private void Straight_line_Click(object sender, RoutedEventArgs e)
        {
            drawing = false;
            points_left = 2;
            Draw.IsChecked = false;
            Window4 b = new Window4
            {
                Owner = this
            };
            b.Title += "straight line";
            b.ShowDialog();
            counter++;
            dots[counter] = b.point_1_name;
            counter++;
            dots[counter] = b.point_2_name;
            counter1++;
            lenghths[counter1] = b.length_value; 
            drawing_now = 2;
            straight = true;
        }

        private void Arbitrary_triangle_Click(object sender, RoutedEventArgs e)
        {
            drawing = false;
            points_left = 3;
            Draw.IsChecked = false;
            Window6 b = new Window6
            {
                Owner = this
            };
            b.ShowDialog();
            drawing_now = 3;
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            Window7 s = new Window7
            {
                Owner = this
            };
            s.ShowDialog();
        }
    }
}
