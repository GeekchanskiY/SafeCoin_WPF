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
    /// Логика взаимодействия для ReviewAdminWindow.xaml
    /// </summary>

   
    public partial class ReviewAdminWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public ReviewAdminWindow()
        {
            InitializeComponent();
        }
    

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            // db.Users.Load();
            //db.Cryptos.Load();
            //db.Shots.Load();
            db.Reviews.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Reviews.Local.ToObservableCollection();

        }
    }
}
