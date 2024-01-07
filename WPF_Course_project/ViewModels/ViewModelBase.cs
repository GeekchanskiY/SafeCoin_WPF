using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Course_project.Models;

namespace WPF_Course_project.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ApplicationContext db = new ApplicationContext();
        public String DisplayName { get; set; }

        public ViewModelBase() {

            db.Database.EnsureCreated();
            
        }

        #region INotifyPropertyChanged Members

        protected void RaisePropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
