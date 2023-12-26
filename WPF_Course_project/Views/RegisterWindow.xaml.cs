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
using System.Text.RegularExpressions;


namespace WPF_Course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    
    public partial class RegisterWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public ObservableCollection<User> Users { get; set; }

        private bool isDragging = false;
        private Point startPoint;
        

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // Close();
            Application.Current.Shutdown();
        }

        


        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void username_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text == "Username")
            {
                username.Text = "";
            }
        }

        private void email_Click(object sender, RoutedEventArgs e)
        {
            if (email.Text == "Email")
            {
                email.Text = "";
            }
        }
        private void about_Click(object sender, RoutedEventArgs e)
        {
            if (aboutMe.Text == "About Me")
            {
                aboutMe.Text = "";
            }
        }

        private void password_Click(object sender, RoutedEventArgs e)
        {
            if (password.Text == "Password")
            {
                password.Text = "";
            }
        }

        private void rpassword_Click(object sender, RoutedEventArgs e)
        {
            if (rpassword.Text == "Repeat password")
            {
                rpassword.Text = "";
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text == "")
            {
                validationError.Content = "Username cant be empty!";
                return;
            }
            if (username.Text.Length <= 5)
            {
                validationError.Content = "Username cant be less 5 symbols!";
                return;
            }
            if (rpassword.Text != password.Text)
            {
                validationError.Content = "Passwords does not match!";
                return;
            }
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email.Text, pattern))
            {
                validationError.Content = "Invalid email!";
                return;
            }

            User? existingUser = db.Users.FirstOrDefault(u => u.Username == username.Text);

            if (existingUser != null)
            {
                validationError.Content = "User with this username already exists!";
                return;
            }

            existingUser = db.Users.FirstOrDefault(u => u.Email == email.Text);

            if (existingUser != null)
            {
                validationError.Content = "User with this email already exists!";
                return;
            }

            User newUser = new User
            {
                Username = username.Text,
                Password = password.Text,
                Email = email.Text,
                AboutMe = aboutMe.Text
            };
            db.Users.Add(newUser);
            db.SaveChanges();

            // Update your ObservableCollection if needed
            // Users.Add(newUser);

            App.CurrentUser = newUser;
            DialogResult = true;
        }




    }

}
