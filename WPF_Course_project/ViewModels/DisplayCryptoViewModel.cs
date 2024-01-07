using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using WPF_Course_project.Models;

namespace WPF_Course_project.ViewModels
{
    // Class to display values inside the list. Do not change
    public class DateModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }

    internal class DisplayCryptoViewModel : ViewModelBase
    {
        private int CryptoId { get; set; }
        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Image { get; set; }

        public float Price { get; set; }

        public float Volume { get; set; }

        public float MarketCap { get; set; }

        public float TransactionsCount { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> Formatter { get; set; } = value => new DateTime((long)value).ToString("dd.MM.yyyy HH:mm");

        public DisplayCryptoViewModel(Crypto c) : base()
        {
            db.Database.EnsureCreated();
            db.Cryptos.Load();
            db.Shots.Load();
            db.Reviews.Load();
            this.CryptoId = c.Id;
            this.Name = c.Name;
            this.Symbol = c.Symbol;
            this.Image = c.Image;
            this.Price = c.Price;
            this.Volume = c.Volume;
            this.MarketCap = c.MarketCap;
            this.TransactionsCount = c.TransactionsCount;
            var mapper = Mappers.Xy<DateModel>()
               .X(dateModel => dateModel.DateTime.Ticks)
               .Y(dateModel => dateModel.Value);
            Charting.For<DateModel>(mapper);

            SeriesCollection = new SeriesCollection
        {
            new LineSeries
            {
                Values = new ChartValues<DateModel>(this.readCryptoShots()),
                Fill = Brushes.Transparent
            }
        };

        }

        private List<DateModel> readCryptoShots()
        {
            int counter = 0;
            List<DateModel> dataPoints = new List<DateModel>();
            ObservableCollection<Shot> shots = new ObservableCollection<Shot>(db.Shots.Where(shot => shot.CryptoId == this.CryptoId).ToList());
            foreach (Shot s in shots)
            {
                counter += 1;
                dataPoints.Add(new DateModel { DateTime = s.Time, Value = s.Price });


            };

            return dataPoints;
        }

    }
}
