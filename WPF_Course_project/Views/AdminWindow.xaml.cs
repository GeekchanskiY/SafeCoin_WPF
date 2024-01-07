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
using WPF_Course_project.Views.Pages;

namespace WPF_Course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public AdminWindow()
        {
            InitializeComponent();

            DisplayAdminMain.Content = new ShowUsersPage();
            ListUsers.IsEnabled = false;
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            // Application.Current.Shutdown();
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

        private void ShowUsersPage_Click(object sender, RoutedEventArgs e)
        {
            ListUsers.IsEnabled = false;
            ListCrypto.IsEnabled = true;
            ListReviews.IsEnabled = true;
            DisplayAdminMain.Content = new ShowUsersPage();

        }
        private void ShowCryptoPage_Click(object sender, RoutedEventArgs e)
        {
            ListUsers.IsEnabled = true;
            ListCrypto.IsEnabled = false;
            ListReviews.IsEnabled = true;
            DisplayAdminMain.Content = new ShowCryptosPage();
        }
        private void ShowReviewsPage_Click(object sender, RoutedEventArgs e)
        {
            ListUsers.IsEnabled = true;
            ListCrypto.IsEnabled = true;
            ListReviews.IsEnabled = false;
            DisplayAdminMain.Content = new ShowReviewPage();
        }





        // добавление

    }
}
