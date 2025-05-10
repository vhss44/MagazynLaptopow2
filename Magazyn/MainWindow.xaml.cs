using Biblioteka;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Security;
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
using Dapper;

namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Laptop> Laptopy { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Laptopy = ZaladujLaptopy();
            SQLiteDataAccess.SprawdzICzyUzupelnijBaze();
        }

        private static string LoadConnectionString()
        {
            return "Data Source=DemoDB.db;Version=3;";
        }
        public List<Laptop> ZaladujLaptopy()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.Query<Laptop>("SELECT * FROM Laptopy").ToList();
            }
        }

        private void Button_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }


        private void btnS_MouseEnter(object sender, MouseEventArgs e)
        {
            btnS.Background = Brushes.Transparent;
        }

        private void btnS_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnS.Background = Brushes.Transparent;
        }

        private void btnS_MouseLeave(object sender, MouseEventArgs e)
        {
            btnS.Background = Brushes.Transparent;
        }

        private void MagazynBtn_Click(object sender, RoutedEventArgs e)
        {
            MagazynLista mainWindow = new MagazynLista();
            mainWindow.Show();
            this.Close();
        }

        private void RaportBtn_Click(object sender, RoutedEventArgs e)
        {

            var raport = new Raport();
            raport.Show();
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

        private void DodajProduktBtn_Click(object sender, RoutedEventArgs e)
        {
            DodajProdukt mainWindow = new DodajProdukt();
            mainWindow.Show();
        
        }
    }

}
