using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Course_project.Models
{
    public class Crypto: INotifyPropertyChanged
    {
        public Crypto() {
        
        }

        private int id;
        public int Id
        {
            get => id;
            // set { id = value; OnPropertyChanged("Id"); }
        }

        private string name;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged("Name"); }
        }
        private string symbol;
        public string Symbol
        {
            get => symbol;
            set { symbol = value; OnPropertyChanged("Symbol"); }
        }
        private string image;
        public string Image
        {
            get => image;
            set { image = value; OnPropertyChanged("Image"); }
        }
        private float price;
        public float Price
        {
            get => price;
            set { price = value; OnPropertyChanged("Price"); }
        }
        private float volume;
        public float Volume
        {
            get => volume;
            set { volume = value; OnPropertyChanged("Volume"); }
        }
        private float marketCap;
        public float MarketCap
        {
            get => marketCap;
            set { marketCap = value; OnPropertyChanged("MarketCap"); }
        }
        private int transactionsCount;
        public int TransactionsCount
        {
            get => transactionsCount;
            set { transactionsCount = value; OnPropertyChanged("TransactionsCount"); }
        }

        public List<Review> Reviews { get; set; }
        // public virtual Review Review { get; set; }

        public List<Shot> Shots { get; set; }

        public List<CryptoSub> CryptoSubs { get; set; }

        public event PropertyChangedEventHandler PropertyChanged; //Событие, которое будет вызвано при изменении модели 
        public void OnPropertyChanged([CallerMemberName] string prop = "") //Метод, который скажет ViewModel, что нужно передать виду новые данные
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class CryptoSub: INotifyPropertyChanged
    {
        private int id;
        public int Id {
            get => id;
            set { id = value; OnPropertyChanged("CryptoSubId"); }
        }
        private string param;
        public string Param
        {
            get => param;
            set { param = value; OnPropertyChanged("Param"); }
        }

        private string requiredValue;
        public string RequiredValue
        {
            get => requiredValue;
            set { requiredValue = value; OnPropertyChanged("RequiredValue"); }
        }

        private string lastValue;
        public string LastValue
        {
            get => lastValue;
            set { lastValue = value; OnPropertyChanged("Value"); }
        }

        public int CryptoId { get; set; }
        public Crypto Crypto { get ; set; }

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
