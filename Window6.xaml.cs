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
using System.Windows.Shapes;

namespace GeomAns
{
    /// <summary>
    /// Логика взаимодействия для Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        public string point_1_name = "";
        public string point_2_name = "";
        public string point_3_name = "";
        public string length_1 = "";
        public string length_2 = "";
        public string length_3 = "";
        public string angle_1 = "";
        public string angle_2 = "";
        public string angle_3 = "";
        public double length_1_value;
        public double length_2_value;
        public double length_3_value;
        public double angle_1_value;
        public double angle_2_value;
        public double angle_3_value;
        public bool condition1;
        public bool condition2;
        public bool condition3;
        public bool condition4;
        public bool condition5;
        public bool condition6;

        public Window6()
        {
            InitializeComponent();
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            point_1_name = Point_1_name.Text;
            point_2_name = Point_2_name.Text;
            point_3_name = Point_3_name.Text;
            string l1 = Lenght_1.Text;
            string l2 = Lenght_2.Text;
            string l3 = Lenght_3.Text;
            string l4 = Angle_1.Text;
            string l5 = Angle_2.Text;
            string l6 = Angle_3.Text;
            condition1 = double.TryParse(l1, out length_1_value);
            condition2 = double.TryParse(l2, out length_2_value);
            condition3 = double.TryParse(l3, out length_3_value);
            condition4 = double.TryParse(l4, out angle_1_value);
            condition5 = double.TryParse(l5, out angle_2_value);
            condition6 = double.TryParse(l6, out angle_3_value);
            this.Close();
        }
    }
}
