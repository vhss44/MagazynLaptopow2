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
        private List<Laptop> _laptopy; // Dodaj pole klasy

        public Raport(List<Laptop> laptopy)
        {
            InitializeComponent();
            _laptopy = laptopy; // Przypisz laptopy do pola klasy

            // Inicjalizuj właściwości przed użyciem
            SeriesCollection = new SeriesCollection();
            Labels = new List<string>();

            DataContext = null;
            DataContext = this;
            GenerujRaport(laptopy ?? new List<Laptop>()); // Zabezpieczenie przed null
        }

        private void GenerujRaport(List<Laptop> laptopy)
        {
            // Sprawdź dane
            if (!laptopy.Any())
            {
                txtRaport.Text = "Brak danych do wyświetlenia";
                return;
            }

            // 1. Podsumowanie tekstowe
            txtRaport.Text = $"Raport z dnia: {DateTime.Now}\n\n" +
                           $"Łączna liczba laptopów: {laptopy.Sum(l => l.IloscSztuk)}\n" +
                           $"Liczba modeli: {laptopy.Count}\n" +
                           $"Unikalnych marek: {laptopy.Select(l => l.Marka).Distinct().Count()}";

            // 2. Laptopy na wyczerpaniu
            // Zmodyfikuj fragment z filtrowaniem
            var laptopyNaWyczerpaniu = laptopy
                .Where(l => l.IloscSztuk < 3)
                .ToList();

            // Dodaj sprawdzenie
            if (!laptopyNaWyczerpaniu.Any())
            {
                MessageBox.Show("Brak laptopów na wyczerpaniu!");
            }

            dgMalolaptop.ItemsSource = laptopyNaWyczerpaniu;
            // 3. Przygotowanie wykresu
            var daneWykresu = laptopy
       .GroupBy(l => l.Marka)
       .Select(g => new { Marka = g.Key, Ilosc = g.Sum(l => l.IloscSztuk) })
       .OrderByDescending(x => x.Ilosc)
       .ToList();

            // Proste etykiety - tylko marki na osi X
            Labels = daneWykresu.Select(x => x.Marka).ToList();

            SeriesCollection.Clear();
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Laptopy",
                Values = new ChartValues<int>(daneWykresu.Select(x => x.Ilosc)),

                // Dodaj tylko te dwie linie
                DataLabels = true,
                LabelPoint = point => $"{point.Y}" // Pokazuje tylko liczby nad kolumnami
            });


        }


        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void MagazynBtn_Click(object sender, RoutedEventArgs e)
        {
            MagazynLista mainWindow = new MagazynLista(_laptopy);
            mainWindow.Show();
            this.Close();
        }


        private void InformacjeBtn_Click(object sender, RoutedEventArgs e)
        {
            Informacje mainWindow = new Informacje(_laptopy);
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
