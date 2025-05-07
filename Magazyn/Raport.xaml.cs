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
    /// <summary>
    /// Logika interakcji dla klasy Raport.xaml
    /// </summary>
    public partial class Raport : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }

        // JEDYNY KONSTRUKTOR
        public Raport(List<Laptop> laptopy)
        {
            InitializeComponent(); // BRAKOWAŁO TEJ LINII!
            DataContext = this;
            GenerujRaport(laptopy);
        }

        private void GenerujRaport(List<Laptop> laptopy) // poprawiona nazwa parametru (mała litera)
        {
            // Sprawdź czy lista nie jest pusta/null
            if (laptopy == null || !laptopy.Any())
            {
                MessageBox.Show("Brak danych do raportu!");
                return;
            }

            // 1. Podsumowanie tekstowe
            txtRaport.Text = $"Raport z dnia: {DateTime.Now}\n\n" +
                           $"Łączna liczba laptopów: {laptopy.Sum(l => l.IloscSztuk)}\n" +
                           $"Liczba modeli: {laptopy.Count}\n" +
                           $"Unikalnych marek: {laptopy.Select(l => l.Marka).Distinct().Count()}";

            // 2. Laptopy na wyczerpaniu
            dgMalolaptop.ItemsSource = laptopy.Where(l => l.IloscSztuk < 3).ToList();

            // 3. Przygotowanie wykresu
            var daneWykresu = laptopy
                .GroupBy(l => l.Marka)
                .Select(g => new { Marka = g.Key, Ilosc = g.Sum(l => l.IloscSztuk) })
                .OrderByDescending(x => x.Ilosc);

            Labels = daneWykresu.Select(x => x.Marka).ToList();
            SeriesCollection = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Laptopy",
                Values = new ChartValues<int>(daneWykresu.Select(x => x.Ilosc))
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
    }
}
