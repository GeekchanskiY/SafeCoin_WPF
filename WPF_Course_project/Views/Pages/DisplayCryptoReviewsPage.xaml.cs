using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Course_project.Models;

namespace WPF_Course_project.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DisplayCryptoReviewsPage.xaml
    /// </summary>
    public partial class DisplayCryptoReviewsPage : Page
    {
        ApplicationContext db = new ApplicationContext();
        public Crypto c { get; set; }
        public DisplayCryptoReviewsPage(Crypto crypto)
        {
            InitializeComponent();
            c = crypto;
            DataContext = new ObservableCollection<Review>(db.Reviews.Where(r => r.CryptoId == crypto.Id).ToList());
        }
        public void ReviewAddClick(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser is null)
            {
                MessageBox.Show("You should login first!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ReviewWindow win = new ReviewWindow(new Review { CryptoId = c.Id, UserId = App.CurrentUser.Id });
            if (win.ShowDialog() == true)
            {
                Review crypto = win.Review;
                if (db.Cryptos.Find(crypto.CryptoId) == null || win.Review.CryptoId != c.Id)
                {
                    MessageBox.Show("Incorrect crypto Id", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (db.Users.Find(crypto.UserId) == null || win.Review.UserId != App.CurrentUser.Id)
                {
                    MessageBox.Show("Incorrect user Id", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (crypto.Time >= DateTime.Now || crypto.Time <= DateTime.MinValue)
                {
                    crypto.Time = DateTime.Now;
                }
                db.Reviews.Add(crypto);
                db.SaveChanges();
                reviewList.Items.Refresh();

            }
            else
            {
                MessageBox.Show("Unexpected error happened", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
    }
}
