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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public string theme = "light";
        public int size = 2;
        public Window1()
        {
            InitializeComponent();
        }

        private void Apply_call(object sender, RoutedEventArgs e)
        {
            if (Dark_theme.IsChecked == true)
            {
                theme = "dark";
                this.Close();
            }

            else
            {
                theme = "light";
                this.Close();
            }
        }
    }
}
