using Microsoft.EntityFrameworkCore;
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
            //db.Shots.Load();
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
            Crypto? user = cryptoList.SelectedItem as Crypto;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;

            CryptoWindow UserWindow = new CryptoWindow(user);

            if (UserWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                user = db.Cryptos.Find(UserWindow.Crypto.Id);
                if (user != null)
                {
                    user.Name = UserWindow.Crypto.Name;
                    user.Symbol = UserWindow.Crypto.Symbol;
                    user.Image = UserWindow.Crypto.Image;
                    user.Price = UserWindow.Crypto.Price;
                    user.Volume = UserWindow.Crypto.Volume;
                    user.Symbol = UserWindow.Crypto.Symbol;
                    user.MarketCap = UserWindow.Crypto.MarketCap;
                    user.TransactionsCount = UserWindow.Crypto.TransactionsCount;
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
