using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF_Course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public ObservableCollection<User> Users { get; set; }
        private bool isDragging = false;
        private Point startPoint;
        public LoginWindow()
        {
            InitializeComponent();
            db.Database.EnsureCreated();
            db.Users.Load();
            Users = db.Users.Local.ToObservableCollection();
        }
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
        

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // Close();
            Application.Current.Shutdown();
        }

       

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (usernameInput.Text == "admin" && passwordInput.Text == "admin")
            {
                User? existingUser = db.Users.FirstOrDefault(u => u.Username == "admin");

                if (existingUser != null)
                {
                    // User exists, you can handle the existing user as needed
                    // MessageBox.Show("User already exists");
                }
                else
                {
                    // User doesn't exist, create a new user
                    User newUser = new User {
                        Username = "admin",
                        Password = "admin",
                        Email = "admin@mail.ru",
                        AboutMe = "I am admin",
                        IsAdmin = true
                    };

                    // Add the new user to the database
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    // Update your ObservableCollection if needed
                    // Users.Add(newUser);
                    existingUser = newUser;

                    // MessageBox.Show("User created successfully");
                }
                App.CurrentUser = existingUser;
                // MessageBox.Show("Login success", App.CurrentUser.IsAdmin.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            } else
            {
                User? existingUser = db.Users.FirstOrDefault(u => u.Username == usernameInput.Text);

                if (existingUser != null)
                {
                    if (existingUser.Password == passwordInput.Text)
                    {
                        App.CurrentUser = existingUser;
                        Close();
                    } else
                    {
                        validationError.Content = "Incorrect password!";
                    }
                } else
                {
                    validationError.Content = "User does not exists!";
                }
            }

            
        }

        private void password_Click(object sender, RoutedEventArgs e)
        {
            if (passwordInput.Text == "Password")
            {
                passwordInput.Text = "";
            }
        }

        private void username_Click(object sender, RoutedEventArgs e)
        {
            if (usernameInput.Text == "Username")
            {
                usernameInput.Text = "";
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            if (registerWindow.ShowDialog() == true)
            {
                if (App.CurrentUser != null)
                {
                   Close();
                }
                
            }
        }
    }
}
