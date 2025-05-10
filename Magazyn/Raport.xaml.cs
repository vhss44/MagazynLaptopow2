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
using Biblioteka;
using LiveCharts;
using LiveCharts.Wpf;
using Dapper;
using System.Data.SQLite;

namespace Magazyn
{
    public partial class Raport : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }

        public Raport()
        {
            InitializeComponent();
            DataContext = this;
            ZaladujIZaktualizujDane();
        }

        private void ZaladujIZaktualizujDane()
        {
        
            var laptopy = SQLiteDataAccess.ZaladujLaptopy();
            GenerujRaport(laptopy);
        }

        private void GenerujRaport(List<Laptop> laptopy)
        {
            if (!laptopy.Any())
            {
                txtRaport.Text = "Brak danych do wyświetlenia";
                return;
            }

        
            txtRaport.Text = $"Raport z dnia: {DateTime.Now}\n\n" +
                           $"Łączna liczba laptopów: {laptopy.Sum(l => l.IloscSztuk)}\n" +
                           $"Liczba modeli: {laptopy.Count}\n" +
                           $"Unikalnych marek: {laptopy.Select(l => l.Marka).Distinct().Count()}";

            var laptopyNaWyczerpaniu = laptopy.Where(l => l.IloscSztuk < 3).ToList();
            dgMalolaptop.ItemsSource = laptopyNaWyczerpaniu;

            
            var daneWykresu = laptopy
                .GroupBy(l => l.Marka)
                .Select(g => new { Marka = g.Key, Ilosc = g.Sum(l => l.IloscSztuk) })
                .OrderByDescending(x => x.Ilosc)
                .ToList();

            Labels = daneWykresu.Select(x => x.Marka).ToList();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Laptopy",
                    Values = new ChartValues<int>(daneWykresu.Select(x => x.Ilosc)),
                    DataLabels = true
                }
            };
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

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var laptopy = SQLiteDataAccess.ZaladujLaptopy();
                GenerujRaport(laptopy);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd ładowania danych: {ex.Message}");
            }

        }
    }
}
