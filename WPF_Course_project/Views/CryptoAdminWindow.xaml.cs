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
using WPF_Course_project.Views;
using Microsoft.EntityFrameworkCore;


namespace WPF_Course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для CryptoAdminWindow.xaml
    /// </summary>
    
    public partial class CryptoAdminWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public CryptoAdminWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // Close();
            Application.Current.Shutdown();
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
            UserWindow UserWindow = new UserWindow(new User());
            if (UserWindow.ShowDialog() == true)
            {
                User User = UserWindow.User;
                db.Users.Add(User);
                db.SaveChanges();
            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Crypto? user = userList.SelectedItem as Crypto;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;

            CryptoWindow UserWindow = new CryptoWindow(user);

            if (UserWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                user = db.Users.Find(UserWindow.User.Id);
                if (user != null)
                {
                    user.Username = UserWindow.User.Username;
                    user.Email = UserWindow.User.Email;
                    user.Password = UserWindow.User.Password;
                    user.AboutMe = UserWindow.User.AboutMe;
                    db.SaveChanges();
                    usersList.Items.Refresh();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Crypto? user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;
            db.Users.Remove(user);
            db.SaveChanges();
        }

    }
}
