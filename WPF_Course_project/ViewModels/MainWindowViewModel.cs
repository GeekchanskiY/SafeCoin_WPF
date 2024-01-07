using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Course_project.Models;

namespace WPF_Course_project.ViewModels
{
    internal class MainWindowViewModel: ViewModelBase
    {
        public ObservableCollection<Crypto> cryptos;
        ApplicationContext db = new ApplicationContext();
        public MainWindowViewModel() : base() {
            db.Cryptos.Load();

        }
    }
}
