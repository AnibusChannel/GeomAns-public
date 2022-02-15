using System.Windows;

namespace GeomAns
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public string point_1_name = "";
        public Window3()
        {
            InitializeComponent();
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            point_1_name = Point_1_name.Text;
            this.Close();
        }
    }
}
