using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using WPF_Course_project.Views.Pages;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;


namespace WPF_Course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для DisplayCryptoWindow.xaml
    /// </summary>
    public class ImageUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string imageUrl)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                try
                {
                    bitmapImage.UriSource = new Uri(imageUrl, UriKind.Absolute);
                }
                catch (System.UriFormatException)
                {
                    return null;
                }
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                try
                {
                    bitmapImage.EndInit();
                }
                catch (System.NotSupportedException)
                {
                    return null;
                }
                return bitmapImage;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class DisplayCryptoDataContext
    {
        public Crypto crypto { get; set; }
        public ObservableCollection<Shot> shots { get; set; }
        public ObservableCollection<Review> reviews { get; set; }

        // Initialize ChartValues in the constructor
        public ChartValues<double> values { get; set; } = new ChartValues<double>();
    }

    public partial class DisplayCryptoWindow : Window
    {
        public Crypto c { get; set; }
        public DisplayCryptoWindow(Crypto crypto)
        {
            InitializeComponent();
            c = crypto;
            DisplayCryptoMain.Content = new DisplayCryptoMainPage(crypto);
            
        }
 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            // Application.Current.Shutdown();
        }
        private void CryptoShowClick(object sender, RoutedEventArgs e)
        {
            DisplayCryptoMain.Content = new DisplayCryptoMainPage(c);
        }
        private void ReviewShowClick(object sender, RoutedEventArgs e)
        {
            DisplayCryptoMain.Content = new DisplayCryptoReviewsPage(c);
        }
    }
}
