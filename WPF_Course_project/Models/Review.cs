using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Course_project.Models
{
    public class Review: INotifyPropertyChanged
    {
        public Review()
        {

        }

        private int id;
        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string title;
        private string text;
        private DateTime time;

        public int CryptoId { get; set; }
        public Crypto Crypto { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public event PropertyChangedEventHandler PropertyChanged; //Событие, которое будет вызвано при изменении модели 
        public void OnPropertyChanged([CallerMemberName] string prop = "") //Метод, который скажет ViewModel, что нужно передать виду новые данные
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
