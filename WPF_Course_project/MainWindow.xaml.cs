using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace WPF_Course_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            
            
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Buttonz.Content = await App.Me.get_json("http://127.0.0.1:8000/api/cryptos/");
        }
    }
}
