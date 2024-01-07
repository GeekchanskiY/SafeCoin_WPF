using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WPF_Course_project.Models;
using WPF_Course_project.Views;

namespace WPF_Course_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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
    public class MillionsConverter : IValueConverter
    {
        public static float StringToFloat(string value)
        {
            if (value.EndsWith("K"))
            {
                return float.Parse(value.Substring(0, value.Length - 1)) * 1000f;
            }
            else if (value.EndsWith("M"))
            {
                return float.Parse(value.Substring(0, value.Length - 1)) * 1000000f;
            }
            else if (value.EndsWith("B"))
            {
                return float.Parse(value.Substring(0, value.Length - 1)) * 1000000000f;
            }
            else
            {
                return float.Parse(value);
            }
        }

        public static string FloatToString(float value)
        {
            if (value < 0.1f)
            {
                return value.ToString("0.00");
            }
            else if (value < 1000f)
            {
                return value.ToString("0.##");
            }
            else if (value < 1000000f)
            {
                return (value / 1000f).ToString("0.##") + "K";
            }
            else if (value < 1000000000f)
            {
                return (value / 1000000f).ToString("0.##") + "M";
            }
            else
            {
                return (value / 1000000000f).ToString("0.##") + "B";
            }
        }


        public object Convert(object val, Type targetType, object parameter, CultureInfo culture)
        {

            /*int val;
            if (int.TryParse(value.ToString(), out val))
            {
                return val.ToString();
            }*/
            float value;
            if (float.TryParse(val.ToString(), out value))
            {
                return MillionsConverter.FloatToString(value);

            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public partial class MainWindow : Window
    {

        ApplicationContext db = new ApplicationContext();
        private bool isDragging = false;
        private Point startPoint;
        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(this);
            this.CaptureMouse();
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newPoint = e.GetPosition(this);
                double deltaX = newPoint.X - startPoint.X;
                double deltaY = newPoint.Y - startPoint.Y;

                this.Left += deltaX;
                this.Top += deltaY;

                startPoint = newPoint;
            }
        }

        private void Window_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDragging = false;
            this.ReleaseMouseCapture();
        }

        public MainWindow()
        {


            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            // Buttonz.Content = await App.Me.get_json("http://127.0.0.1:8000/api/cryptos/");
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // Close();
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Cryptos.Load();
            var cryptos = db.Cryptos;
            if (cryptos.Count() > 0)
            {
                float lowestPrice = cryptos.Min(p => p.Price);
                float highestPrice = cryptos.Max(p => p.Price);
                float highestCap = cryptos.Max(p => p.MarketCap);
                float lowestCap = cryptos.Min(p => p.MarketCap);
                float lowestVolume = cryptos.Min(p => p.Volume);
                float highestVolume = cryptos.Max(p => p.Volume);
                int highestTran = cryptos.Max(p => p.TransactionsCount);
                int lowestTran = cryptos.Min(p => p.TransactionsCount);
                CapFrom.Text = MillionsConverter.FloatToString(lowestCap);
                CapTo.Text = MillionsConverter.FloatToString(highestCap);
                PriceFrom.Text = MillionsConverter.FloatToString(lowestPrice);
                PriceTo.Text = MillionsConverter.FloatToString(highestPrice);
                VolumeFrom.Text = MillionsConverter.FloatToString(lowestVolume);
                VolumeTo.Text = MillionsConverter.FloatToString(highestVolume);
                TransactionFrom.Text = MillionsConverter.FloatToString(lowestTran);
                TransactionsTo.Text = MillionsConverter.FloatToString(highestTran);
            }
            // и устанавливаем данные в качестве контекста
            DataContext = db.Cryptos.Local.ToObservableCollection();

        }


        private void User_Click(object sender, RoutedEventArgs e)
        {
            // testaddbutton.Content = "asd";
            if (App.CurrentUser == null)
            {
                LoginWindow loginWindow = new LoginWindow();
                if (loginWindow.ShowDialog() == true)
                {
                    if (App.CurrentUser == null)
                    {
                        Application.Current.Shutdown();
                    }
                }
            }
            else
            {

                UserWindow UserWindow = new UserWindow(App.CurrentUser);
                if (UserWindow.ShowDialog() == true)
                {
                    // получаем измененный объект
                    User? user = db.Users.Find(UserWindow.User.Id);
                    if (user != null)
                    {
                        user.Username = UserWindow.User.Username;
                        user.Email = UserWindow.User.Email;
                        user.Password = UserWindow.User.Password;
                        user.AboutMe = UserWindow.User.AboutMe;
                        db.SaveChanges();
                    }
                }

            }
            if (App.CurrentUser != null)
            {
                if (App.CurrentUser.IsAdmin == true)
                {

                    AdminButton.Visibility = Visibility.Visible;
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser != null)
            {
                if (App.CurrentUser.IsAdmin)
                {
                    AdminWindow admin = new AdminWindow();
                    if (admin.ShowDialog() == true)
                    {
                    }
                    db.Cryptos.Load();
                    DataContext = db.Cryptos.Local.ToObservableCollection();

                }
            }
        }
        private void ApplyFilters(object sender, RoutedEventArgs eventArgs)
        {
            List<Crypto> queryset = db.Cryptos.Local.ToList();
            long filterValue;
            if (CapFrom.Text != "From")
            {
                try
                {

                    filterValue = (long)MillionsConverter.StringToFloat(CapFrom.Text);

                    queryset = queryset.Where(crypto => crypto.MarketCap >= filterValue).ToList();
                }
                catch
                {
                    MessageBox.Show("Value error on MarketCap From", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
            if (CapTo.Text != "To")
            {
                try
                {
                    filterValue = (long)MillionsConverter.StringToFloat(CapTo.Text);
                    queryset = queryset.Where(crypto => crypto.MarketCap <= filterValue).ToList();
                }
                catch
                {
                    MessageBox.Show("Value error on MarketCap To", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            if (PriceTo.Text != "To")
            {
                try
                {
                    filterValue = (long)MillionsConverter.StringToFloat(PriceTo.Text);
                    queryset = queryset.Where(crypto => crypto.Price <= filterValue).ToList();
                }
                catch
                {
                    MessageBox.Show("Value error on Price To", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
            if (PriceFrom.Text != "From")
            {
                try
                {
                    filterValue = (long)MillionsConverter.StringToFloat(PriceFrom.Text);
                    queryset = queryset.Where(crypto => crypto.Price >= filterValue).ToList();
                }
                catch
                {
                    MessageBox.Show("Value error on Price From", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            if (TransactionFrom.Text != "From")
            {
                try
                {
                    filterValue = (long)MillionsConverter.StringToFloat(TransactionFrom.Text);
                    queryset = queryset.Where(crypto => crypto.TransactionsCount >= filterValue).ToList();
                }
                catch
                {
                    MessageBox.Show("Value error on Transactions From", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
            if (TransactionsTo.Text != "To")
            {
                try
                {
                    filterValue = (long)MillionsConverter.StringToFloat(TransactionsTo.Text);
                    queryset = queryset.Where(crypto => crypto.TransactionsCount <= filterValue).ToList();
                }
                catch
                {
                    MessageBox.Show("Value error on Transactions To", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
            if (VolumeFrom.Text != "From")
            {
                try
                {
                    filterValue = (long)MillionsConverter.StringToFloat(VolumeFrom.Text);
                    queryset = queryset.Where(crypto => crypto.Volume >= filterValue).ToList();
                }
                catch
                {
                    MessageBox.Show("Value error on Volume From", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
            if (VolumeTo.Text != "To")
            {
                try
                {
                    filterValue = (long)MillionsConverter.StringToFloat(VolumeTo.Text);
                    queryset = queryset.Where(crypto => crypto.Volume <= filterValue).ToList();
                }
                catch
                {
                    MessageBox.Show("Value error on Volume To", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
            if (NameInput.Text != "Name Here")
            {

                queryset = queryset.Where(crypto => crypto.Name.Contains(NameInput.Text, StringComparison.OrdinalIgnoreCase)).ToList();


            }
            DataContext = new ObservableCollection<Crypto>(queryset);
        }

        private void SelectClick(object sender, RoutedEventArgs e)
        {
            Crypto? crypto = cryptoList.SelectedItem as Crypto;
            // если ни одного объекта не выделено, выходим
            if (crypto is null) return;

            DisplayCryptoWindow displayx = new DisplayCryptoWindow(crypto);
            if (displayx.ShowDialog() == true)
            {

            }

        }

        private void ResetFilters(object sender, RoutedEventArgs e)
        {
            var cryptos = db.Cryptos;
            if (cryptos.Count() > 0)
            {
                float lowestPrice = cryptos.Min(p => p.Price);
                float highestPrice = cryptos.Max(p => p.Price);
                float highestCap = cryptos.Max(p => p.MarketCap);
                float lowestCap = cryptos.Min(p => p.MarketCap);
                float lowestVolume = cryptos.Min(p => p.Volume);
                float highestVolume = cryptos.Max(p => p.Volume);
                int highestTran = cryptos.Max(p => p.TransactionsCount);
                int lowestTran = cryptos.Min(p => p.TransactionsCount);
                CapFrom.Text = MillionsConverter.FloatToString(lowestCap);
                CapTo.Text = MillionsConverter.FloatToString(highestCap);
                PriceFrom.Text = MillionsConverter.FloatToString(lowestPrice);
                PriceTo.Text = MillionsConverter.FloatToString(highestPrice);
                VolumeFrom.Text = MillionsConverter.FloatToString(lowestVolume);
                VolumeTo.Text = MillionsConverter.FloatToString(highestVolume);
                TransactionFrom.Text = MillionsConverter.FloatToString(lowestTran);
                TransactionsTo.Text = MillionsConverter.FloatToString(highestTran);
            }
            // и устанавливаем данные в качестве контекста

            DataContext = db.Cryptos.Local.ToObservableCollection();
        }

        private void NotificationsClick(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser is null)
            {
                MessageBox.Show("Authentificate first!", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.CryptoSubs.Where(c => c.UserId == App.CurrentUser.Id).Count() == 0)
            {
                MessageBox.Show("No available notifications!", "ValueError", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int not_notified = 0;
            float last_value = 0;
            float required_value = 0;
            Crypto rc;
            foreach (CryptoSub c in db.CryptoSubs.Where(c => c.UserId == App.CurrentUser.Id))
            {
                try
                {
                    rc = db.Cryptos.Where(z => z.Id == c.CryptoId).First();
                    if (float.TryParse(c.LastValue, out last_value) && float.TryParse(c.RequiredValue, out required_value))
                    {
                        if (c.Param == "Vol")
                        {
                            if (last_value < required_value)
                            {
                                if (required_value > rc.Volume)
                                {
                                    MessageBox.Show("Volume raised to required value: " + c.RequiredValue, rc.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                                    db.CryptoSubs.Remove(c);
                                }
                                else
                                {
                                    not_notified++;
                                }
                            }
                            else
                            {
                                if (rc.Volume < required_value)
                                {
                                    MessageBox.Show("Volume dropped to required value: " + c.RequiredValue, rc.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                                    db.CryptoSubs.Remove(c);
                                }
                                else
                                {
                                    not_notified++;
                                }
                            }
                        }
                        if (c.Param == "Price")
                        {
                            if (last_value < required_value)
                            {
                                if (required_value > rc.Price)
                                {
                                    MessageBox.Show("Price raised to required value: " + c.RequiredValue, rc.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                                    db.CryptoSubs.Remove(c);
                                }
                                else
                                {
                                    not_notified++;
                                }
                            }
                            else
                            {
                                if (rc.Price < required_value)
                                {
                                    MessageBox.Show("Price dropped to required value: " + c.RequiredValue, rc.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                                    db.CryptoSubs.Remove(c);
                                }
                                else
                                {
                                    not_notified++;
                                }
                            }
                        }
                        if (c.Param == "MKT")
                        {
                            if (last_value < required_value)
                            {
                                if (required_value > rc.MarketCap)
                                {
                                    MessageBox.Show("Market cap raised to required value: " + c.RequiredValue, rc.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                                    db.CryptoSubs.Remove(c);
                                }
                                else
                                {
                                    not_notified++;
                                }
                            }
                            else
                            {
                                if (rc.MarketCap < required_value)
                                {
                                    MessageBox.Show("Market cap dropped to required value: " + c.RequiredValue, rc.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                                    db.CryptoSubs.Remove(c);
                                }
                                else
                                {
                                    not_notified++;
                                }
                            }
                        }
                        db.SaveChanges();
                        db.CryptoSubs.Load();

                    }
                }
                catch
                {
                }



            }
            MessageBox.Show("Not changed amount: " + not_notified.ToString(), "Not changed", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
