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
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public string point_1_name = "";
        public string point_2_name = "";
        public string length = "";
        public bool condition;
        public double length_value;
        public Window4()
        {
            InitializeComponent();
        }
        private void Build_Click(object sender, RoutedEventArgs e)
        {
            point_1_name = Point_1_name.Text;
            point_2_name = Point_2_name.Text;
            string l = Length.Text;
            condition = double.TryParse(l, out length_value);
            if (!condition && l == "?")
            {
                length_value = -1;
                this.Close();
            }
            else if(!condition && l == "")
            {
                length_value = 0;
                this.Close();
            }
            else if(condition && length_value <= 0 || !condition && l != "?")
            {
                Window5 er = new Window5
                {
                    Owner = this
                };
                er.Label.Content = "Please enter a valid value";
                er.ShowDialog();
            }
            else
            {
                this.Close();
            }
        }
    }
}
