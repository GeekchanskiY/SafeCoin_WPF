using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
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
using WPF_Course_project.ViewModels;
using WPF_Course_project.Views;

namespace WPF_Course_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
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
            } else {
                WindowState = WindowState.Maximized;
            }            
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Users.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Users.Local.ToObservableCollection();
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
            } else
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
                    MessageBox.Show("Admin panel button appeared on left bottom corner", "Login", MessageBoxButton.OK, MessageBoxImage.Information);

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
                }
            }
        }
    }
}
