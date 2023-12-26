using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Course_project.Models
{
    public class Shot: INotifyPropertyChanged
    {
        private int id;
        public int Id { get => id; set { OnPropertyChanged("Id"); } }

        private DateTime time;

        public DateTime Time { get => time; set { OnPropertyChanged("Time"); } }

        private float price;
        public float Price { get => price; set { price = value; OnPropertyChanged("Price"); } } 

        private float marketCap;
        public float MarketCap { get => marketCap; set { volume = value; OnPropertyChanged("MarketCap"); } }
        private float volume;
        public float Volume { get => volume; set { volume = value; OnPropertyChanged("Volume"); } }
        private int transactions;
        public int Transactions {
            get => transactions;
            set { transactions = value; OnPropertyChanged("Transactions"); }
        }

        public int CryptoId { get; set; }
        public Crypto Crypto { get; set; }




        public event PropertyChangedEventHandler PropertyChanged; //Событие, которое будет вызвано при изменении модели 
        public void OnPropertyChanged([CallerMemberName] string prop = "") //Метод, который скажет ViewModel, что нужно передать виду новые данные
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
