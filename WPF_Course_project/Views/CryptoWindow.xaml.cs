﻿using System;
using System.Collections.Generic;
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

namespace WPF_Course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для CryptoWindow.xaml
    /// </summary>
    public partial class CryptoWindow : Window
    {
        public Crypto Crypto { get; private set; }
        public CryptoWindow(Crypto crypto)
        {
            InitializeComponent();
            Crypto = crypto;
            DataContext = Crypto;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
        }

        void Reviews(object sender, RoutedEventArgs e)
        {

            // DialogResult = true;
        }

        void ShowShots(object sender, RoutedEventArgs e)
        {
            ShotAdminWindow win = new ShotAdminWindow(Crypto);


            win.ShowDialog();
        }
    }
}
