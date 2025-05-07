using Biblioteka;
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
    /// Logika interakcji dla klasy Informacje.xaml
    /// </summary>
    public partial class Informacje : Window
    {
        public List<Laptop> Laptopy { get; set; }
        public Informacje()
        {
            InitializeComponent();
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void MagazynBtn_Click(object sender, RoutedEventArgs e)
        {
            MagazynLista mainWindow = new MagazynLista();
            mainWindow.Show();
            this.Close();
        }

        private void RaportBtn_Click(object sender, RoutedEventArgs e)
        {
            var raport = new Raport(Laptopy); // Laptopy to lista załadowana w konstruktorze MainWindow
            raport.Show();
            this.Close();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.IsOpen = !UserPopup.IsOpen;
        }
        private void DodajProduktBtn_Click(object sender, RoutedEventArgs e)
        {
            DodajProdukt mainWindow = new DodajProdukt();
            mainWindow.Show();

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
