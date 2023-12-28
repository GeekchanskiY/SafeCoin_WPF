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
using WPF_Course_project.Models;

namespace WPF_Course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для ShotWindow.xaml
    /// </summary>
    public partial class ShotWindow : Window
    {
        public Shot Shot { get; private set; }
        public ShotWindow(Shot shot)
        {
            InitializeComponent();
            Shot = shot;
            DataContext = Shot;

        }
        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
