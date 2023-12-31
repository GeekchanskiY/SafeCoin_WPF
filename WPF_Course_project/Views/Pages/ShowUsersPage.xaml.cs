﻿using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using WPF_Course_project.Models;

namespace WPF_Course_project.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShowUsersPage.xaml
    /// </summary>
    public partial class ShowUsersPage : Page
    {
        ApplicationContext db = new ApplicationContext();
        public ShowUsersPage()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Users.Load();
            // db.Cryptos.Load();
            //db.Shots.Load();
            //db.Reviews.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Users.Local.ToObservableCollection();

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
            User? user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;

            UserWindow UserWindow = new UserWindow(user);

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
            User? user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
