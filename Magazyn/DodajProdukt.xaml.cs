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

namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy DodajProdukt.xaml
    /// </summary>
    public partial class DodajProdukt : Window
    {
        public DodajProdukt()
        {
            InitializeComponent();
            ZaladujLaptopy();
        }

        private void ZaladujLaptopy()
        {
            var laptopy = SQLiteDataAccess.ZaladujLaptopy();
            LaptopsListView.ItemsSource = laptopy;
        }

      

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SerialNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(MarkaTextBox.Text) ||
                string.IsNullOrWhiteSpace(ModelTextBox.Text) ||
                OSComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(QuantityTextBox.Text))
            {
                MessageBox.Show("Proszę uzupełnić wszystkie pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int ilosc;
            if (!int.TryParse(QuantityTextBox.Text, out ilosc))
            {
                MessageBox.Show("Ilość sztuk musi być liczbą.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var laptop = new Laptop
            {
                NumerSeryjny = SerialNumberTextBox.Text,
                Marka = MarkaTextBox.Text,
                Model = ModelTextBox.Text,
                SystemOperacyjny = ((ComboBoxItem)OSComboBox.SelectedItem).Content.ToString(),
                IloscSztuk = ilosc
            };

            try
            {
                SQLiteDataAccess.ZapiszLaptop(laptop);
                MessageBox.Show("Laptop dodany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                ZaladujLaptopy(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dodawania: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (LaptopsListView.SelectedItem is Biblioteka.Laptop selectedLaptop)
            {
                var result = MessageBox.Show($"Czy na pewno chcesz usunąć laptopa {selectedLaptop.NumerSeryjny}?",
                                             "Potwierdzenie usunięcia",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Biblioteka.SQLiteDataAccess.UsunLaptop(selectedLaptop.NumerSeryjny);
                    ZaladujLaptopy();
                }
            }
            else
            {
                MessageBox.Show("Najpierw wybierz laptopa do usunięcia.", "Brak wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
