using System;
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

namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy MagazynLista.xaml
    /// </summary>
    public partial class MagazynLista : Window
    {
        public MagazynLista()
        {
            InitializeComponent();
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void RaportBtn_Click(object sender, RoutedEventArgs e)
        {
            Raport mainWindow = new Raport();
            mainWindow.Show();
            this.Close();
        }

        private void InformacjeBtn_Click(object sender, RoutedEventArgs e)
        {
            Informacje mainWindow = new Informacje();
            mainWindow.Show();
            this.Close();
        }
        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.IsOpen = !UserPopup.IsOpen;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.IsOpen = false;
            var result = MessageBox.Show("Czy na pewno chcesz się wylogować?",
                                       "Potwierdzenie",
                                       MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                LoginWindow mainWindow = new LoginWindow();
                mainWindow.Show();
                this.Close();
            }
        }

    }
}
