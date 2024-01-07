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
using Microsoft.EntityFrameworkCore;
using WPF_Course_project.Models;
using System.Linq;
using System.Collections.ObjectModel;


namespace WPF_Course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для ShotAdminWindow.xaml
    /// </summary>
    public partial class ShotAdminWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public Crypto RelatedCrypto { get; private set; }
        public ShotAdminWindow(Crypto crypto)
        {
            InitializeComponent();
            RelatedCrypto = crypto;
            Loaded += MainWindow_Loaded;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            // Application.Current.Shutdown();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            
            db.Shots.Load();
            DataContext = new ObservableCollection<Shot>(db.Shots.Where(shot => shot.CryptoId == RelatedCrypto.Id).ToList());

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // READY
            ShotWindow shotWindow = new ShotWindow(new Shot {
                CryptoId = RelatedCrypto.Id,
                Time = DateTime.Now,
                Price=RelatedCrypto.Price,
                MarketCap=RelatedCrypto.MarketCap,
                Transactions=RelatedCrypto.TransactionsCount,
                Volume=RelatedCrypto.Volume
            });
            if (shotWindow.ShowDialog() == true)
            {
                Shot shot = shotWindow.Shot;
                shot.CryptoId = RelatedCrypto.Id;
                // shot.Crypto = RelatedCrypto;
                db.Shots.Add(shot);
                db.SaveChanges();
                shotList.Items.Refresh();

            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            // получаем выделенный объект
            Shot? shot = shotList.SelectedItem as Shot;
            // если ни одного объекта не выделено, выходим
            if (shot is null) return;

            ShotWindow shotWindow = new ShotWindow(shot);

            if (shotWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                shot = db.Shots.Find(shotWindow.Shot.Id);
                
                if (shot != null)
                {
                   
                    shot.Time = shotWindow.Shot.Time;
                    shot.Price = shotWindow.Shot.Price;
                    shot.MarketCap = shotWindow.Shot.MarketCap;
                    shot.Volume = shotWindow.Shot.Volume;
                    shot.Transactions = shotWindow.Shot.Transactions;
                    db.SaveChanges();
                    shotList.Items.Refresh();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Shot? shot = shotList.SelectedItem as Shot;
            // если ни одного объекта не выделено, выходим
            if (shot is null) return;
            db.Shots.Remove(shot);
            db.SaveChanges();
        }
    }
}
