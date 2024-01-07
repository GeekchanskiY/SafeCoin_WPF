using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Windows.Controls;
using WPF_Course_project.Models;

namespace WPF_Course_project.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShowReviewPage.xaml
    /// </summary>
    public partial class ShowReviewPage : Page
    {
        ApplicationContext db = new ApplicationContext();
        public ShowReviewPage()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            // db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Users.Load();
            db.Cryptos.Load();
            //db.Shots.Load();
            db.Reviews.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Reviews.Local.ToObservableCollection();

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // READY
            ReviewWindow cryptoWindow = new ReviewWindow(new Review { Time = DateTime.Now, UserId = App.CurrentUser.Id });
            if (cryptoWindow.ShowDialog() == true)
            {
                Review crypto = cryptoWindow.Review;
                if (db.Cryptos.Find(crypto.CryptoId) == null)
                {
                    MessageBox.Show("Incorrect crypto Id", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (db.Users.Find(crypto.UserId) == null)
                {
                    MessageBox.Show("Incorrect user Id", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (crypto.Time >= DateTime.Now || crypto.Time <= DateTime.MinValue)
                {
                    crypto.Time = DateTime.Now;
                }
                db.Reviews.Add(crypto);
                db.SaveChanges();
                reviewList.Items.Refresh();

            }
            else
            {
                MessageBox.Show("Unexpected error happened", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            // получаем выделенный объект
            Review? user = reviewList.SelectedItem as Review;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;

            ReviewWindow UserWindow = new ReviewWindow(user);

            if (UserWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                user = db.Reviews.Find(UserWindow.Review.Id);
                if (user != null)
                {

                    // user.TransactionsCount = UserWindow.Crypto.TransactionsCount;
                    user.Text = UserWindow.Review.Text;
                    user.Title = UserWindow.Review.Title;
                    if (UserWindow.Review.Time >= DateTime.Now || UserWindow.Review.Time <= DateTime.MinValue)
                    {
                        user.Time = DateTime.Now;
                    }
                    user.Time = UserWindow.Review.Time;
                    db.SaveChanges();
                    reviewList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Review? user = reviewList.SelectedItem as Review;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;
            db.Reviews.Remove(user);
            db.SaveChanges();
        }
    }
}
