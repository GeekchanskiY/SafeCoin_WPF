using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_Course_project.Models;

namespace WPF_Course_project.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShowCryptosPage.xaml
    /// </summary>

    public partial class ShowCryptosPage : Page
    {
        ApplicationContext db = new ApplicationContext();

        public ShowCryptosPage()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            //db.Users.Load();
            db.Cryptos.Load();
            db.Shots.Load();
            //db.Reviews.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Cryptos.Local.ToObservableCollection();

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // READY
            CryptoWindow cryptoWindow = new CryptoWindow(new Crypto());
            if (cryptoWindow.ShowDialog() == true)
            {
                Crypto crypto = cryptoWindow.Crypto;
                db.Cryptos.Add(crypto);
                
                db.SaveChanges();
                db.Shots.Add(new Shot
                {
                    CryptoId = crypto.Id,
                    MarketCap = crypto.MarketCap,
                    Volume = crypto.Volume,
                    Price = crypto.Price,
                    Transactions = crypto.TransactionsCount,
                    Time = DateTime.Now
                });
                db.SaveChanges();
                cryptoList.Items.Refresh();
                MessageBox.Show(db.Cryptos.Count().ToString(), "Login", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                // MessageBox.Show("Admin panel button appeared on left bottom corner", "Login", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            // получаем выделенный объект
            Crypto? crypto = cryptoList.SelectedItem as Crypto;
            // если ни одного объекта не выделено, выходим
            if (crypto is null) return;

            CryptoWindow UserWindow = new CryptoWindow(crypto);

            if (UserWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                crypto = db.Cryptos.Find(UserWindow.Crypto.Id);
                if (crypto != null)
                {
                    crypto.Name = UserWindow.Crypto.Name;
                    crypto.Symbol = UserWindow.Crypto.Symbol;
                    crypto.Image = UserWindow.Crypto.Image;
                    crypto.Price = UserWindow.Crypto.Price;
                    crypto.Volume = UserWindow.Crypto.Volume;
                    crypto.Symbol = UserWindow.Crypto.Symbol;
                    crypto.MarketCap = UserWindow.Crypto.MarketCap;
                    crypto.TransactionsCount = UserWindow.Crypto.TransactionsCount;
                    db.SaveChanges();
                    db.Shots.Add(new Shot
                    {
                        CryptoId = crypto.Id,
                        MarketCap = crypto.MarketCap,
                        Volume = crypto.Volume,
                        Price = crypto.Price,
                        Transactions = crypto.TransactionsCount,
                        Time = DateTime.Now
                    });
                    db.SaveChanges();
                    cryptoList.Items.Refresh();
                }
            }
        }
        private void Shots_Click(object sender, RoutedEventArgs e)
        {
            Crypto? user = cryptoList.SelectedItem as Crypto;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;
            ShotAdminWindow shotAdminWindow = new ShotAdminWindow(user);
            if (shotAdminWindow.ShowDialog() == true)
            {
                db.SaveChanges();
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Crypto? user = cryptoList.SelectedItem as Crypto;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;
            db.Cryptos.Remove(user);
            db.SaveChanges();
        }
    }
}
