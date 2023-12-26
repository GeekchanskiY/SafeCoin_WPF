using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Course_project.Models;
using Microsoft.EntityFrameworkCore;


namespace WPF_Course_project.ViewModels
{
    public class LoginViewModel
    {
        ApplicationContext db = new ApplicationContext();

        public ObservableCollection<User> Users { get; set; }
        public LoginViewModel()
        {
            db.Database.EnsureCreated();
            db.Users.Load();
            Users = db.Users.Local.ToObservableCollection();
        }
    }
}
