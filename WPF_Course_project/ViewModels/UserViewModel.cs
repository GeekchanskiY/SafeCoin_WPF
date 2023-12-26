using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPF_Course_project.Models;
using Microsoft.EntityFrameworkCore;
using WPF_Course_project.Views;

namespace WPF_Course_project.ViewModels
{
    public class ApplicationViewModel
    {

        ApplicationContext db = new ApplicationContext();
        RelayCommand? addCommand;
        RelayCommand? editCommand;
        RelayCommand? deleteCommand;

        RelayCommand? deleteCryptoCommand;
        RelayCommand? editCryptoCommand;
        RelayCommand? addCryptoCommand;

        RelayCommand? deleteCryptoShotCommand;
        RelayCommand? editCryptoShotCommand;
        RelayCommand? addCryptoShotCommand;

        RelayCommand? deleteReviewCommand;
        RelayCommand? editReviewCommand;
        RelayCommand? addReviewCommand;

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Crypto> Cryptos { get; set; }
        public ObservableCollection<Shot> Shots { get; set; }
        public ObservableCollection<Review> Reviews { get; set; }
        public ApplicationViewModel()
        {
            db.Database.EnsureCreated();
            db.Users.Load();
            Users = db.Users.Local.ToObservableCollection();
            Cryptos = db.Cryptos.Local.ToObservableCollection();
            Shots = db.Shots.Local.ToObservableCollection();
            Reviews = db.Reviews.Local.ToObservableCollection();
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      UserWindow userWindow = new UserWindow(new User());
                      if (userWindow.ShowDialog() == true)
                      {
                          User user = userWindow.User;
                          db.Users.Add(user);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      User? user = selectedItem as User;
                      if (user == null) return;

                      User vm = new User
                      {
                          Username = user.Username,
                          Password = user.Password,
                      
                          Email = user.Email,
                          AboutMe = user.AboutMe
                      };
                      UserWindow userWindow = new UserWindow(vm);


                      if (userWindow.ShowDialog() == true)
                      {
                          user.Username = userWindow.User.Username;
                          // user.Birthday = userWindow.User.Birthday;
                          db.Entry(user).State = EntityState.Modified;
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      User? user = selectedItem as User;
                      if (user == null) return;
                      db.Users.Remove(user);
                      db.SaveChanges();
                  }));
            }
        }
    }
}
