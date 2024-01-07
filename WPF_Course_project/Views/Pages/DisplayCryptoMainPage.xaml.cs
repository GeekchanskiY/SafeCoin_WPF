using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF_Course_project.Models;
using Microsoft.EntityFrameworkCore;
using WPF_Course_project.ViewModels;
using System.Globalization;

namespace WPF_Course_project.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DisplayCryptoMainPage.xaml
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
                } catch (System.UriFormatException) {
                    return null;
                }
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                try
                {
                    bitmapImage.EndInit();
                } catch (System.NotSupportedException) {
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
    public partial class DisplayCryptoMainPage : Page
    {
        ApplicationContext db = new ApplicationContext();
        private Crypto c { get; set; }
        public DisplayCryptoMainPage(Crypto crypto)
        {
            InitializeComponent();
            c = crypto;
            DataContext = new DisplayCryptoViewModel(crypto);
            db.Database.EnsureCreated();

            db.Shots.Load();
            db.CryptoSubs.Load();
        }
        private float sub_value = 0;
        private bool getValue()
        {
            if (App.CurrentUser is null)
            {
                return false;
            }
            if (float.TryParse(SubValue.Text, out sub_value))
            {
                return true;
            } else {
                return false;
            }
        }
        public void SubPrice(object sender, RoutedEventArgs e)
        {
            if (!getValue()) {
                MessageBox.Show("Incorrect data provided or not authentificated", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            CryptoSub s = new CryptoSub();
            s.Param = "Price";
            s.LastValue = c.Price.ToString();
            s.RequiredValue = sub_value.ToString();
            s.CryptoId = c.Id;
            s.UserId = App.CurrentUser.Id;
            db.CryptoSubs.Add(s);
            db.SaveChanges();
        }
        public void SubMKT(object sender, RoutedEventArgs e)
        {
            if (!getValue())
            {
                MessageBox.Show("Incorrect data provided or not authentificated", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            CryptoSub s = new CryptoSub();
            s.Param = "MKT";
            s.LastValue = c.MarketCap.ToString();
            s.RequiredValue = sub_value.ToString();
            s.CryptoId = c.Id;
            s.UserId = App.CurrentUser.Id;
            db.CryptoSubs.Add(s);
            db.SaveChanges();
        }
        public void SubVol(object sender, RoutedEventArgs e)
        {
            if (!getValue())
            {
                MessageBox.Show("Incorrect data provided or not authentificated", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            CryptoSub s = new CryptoSub();
            s.Param = "Vol";
            s.LastValue = c.Volume.ToString();
            s.RequiredValue = sub_value.ToString();
            s.CryptoId = c.Id;
            s.UserId = App.CurrentUser.Id;
            db.CryptoSubs.Add(s);
            db.SaveChanges();
        }

    }
}
