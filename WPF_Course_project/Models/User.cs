using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Course_project.Models
{
    public class User: INotifyPropertyChanged
    {
        private int id;
        public int Id {
            get => id;
            // set { id = value; OnPropertyChanged("Id"); }
        }
        public User()
        {
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            AboutMe = string.Empty;
        }

        public User(string username, string password, string email, string aboutme)
        {
            Username = username;
            Password = password;
            Email = email;
            AboutMe = aboutme;
        }

        private string username;
        public string Username
        {
            get => username;
            set { username = value; OnPropertyChanged("Username"); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged("Password"); }
        }

        private string email;
        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged("Email"); }
        }

        private string aboutme;
        public string AboutMe
        {
            get => aboutme;
            set { aboutme = value; OnPropertyChanged("AboutMe"); }
        }

       

        private bool isAdmin;

        public bool IsAdmin
        {
            get => isAdmin;
            set { isAdmin = value; OnPropertyChanged("IsAdmin"); }
        }

        public List<Review> Reviews { get; set; }

        public event PropertyChangedEventHandler PropertyChanged; //Событие, которое будет вызвано при изменении модели 
        public void OnPropertyChanged([CallerMemberName] string prop = "") //Метод, который скажет ViewModel, что нужно передать виду новые данные
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
