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
    public partial class DodajProdukt : Window
    {
        private Laptop _edytowanyLaptop = null; 

        public DodajProdukt()
        {
            InitializeComponent();
            ZaladujLaptopy();
            UstawTrybDodawania();
        }

        private void ZaladujLaptopy()
        {
            var laptopy = SQLiteDataAccess.ZaladujLaptopy();
            LaptopsListView.ItemsSource = laptopy;
        }

        private void UstawTrybDodawania()
        {
            _edytowanyLaptop = null;
            AddButton.Content = "Dodaj";
            DeleteButton.IsEnabled = true;
            ClearButton.IsEnabled = true;
        }

        private void UstawTrybEdycji(Laptop laptop)
        {
            _edytowanyLaptop = laptop;
            SerialNumberTextBox.Text = laptop.NumerSeryjny;
            MarkaTextBox.Text = laptop.Marka;
            ModelTextBox.Text = laptop.Model;
            OSComboBox.SelectedItem = OSComboBox.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == laptop.SystemOperacyjny);
            QuantityTextBox.Text = laptop.IloscSztuk.ToString();
            AddButton.Content = "Zapisz zmiany";
            DeleteButton.IsEnabled = false; 
            ClearButton.IsEnabled = false; 
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SprawdzPoprawnoscDanych())
                return;

            var laptop = new Laptop
            {
                NumerSeryjny = SerialNumberTextBox.Text,
                Marka = MarkaTextBox.Text,
                Model = ModelTextBox.Text,
                SystemOperacyjny = ((ComboBoxItem)OSComboBox.SelectedItem).Content.ToString(),
                IloscSztuk = int.Parse(QuantityTextBox.Text)
            };

            try
            {
                if (_edytowanyLaptop == null)
                {
                    SQLiteDataAccess.ZapiszLaptop(laptop);
                    MessageBox.Show("Laptop dodany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    SQLiteDataAccess.AktualizujLaptop(_edytowanyLaptop.NumerSeryjny, laptop);
                    MessageBox.Show("Laptop zaktualizowany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    UstawTrybDodawania();
                }
                ZaladujLaptopy();
                WyczyscFormularz();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool SprawdzPoprawnoscDanych()
        {
            if (string.IsNullOrWhiteSpace(SerialNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(MarkaTextBox.Text) ||
                string.IsNullOrWhiteSpace(ModelTextBox.Text) ||
                OSComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(QuantityTextBox.Text))
            {
                MessageBox.Show("Proszę uzupełnić wszystkie pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(QuantityTextBox.Text, out _))
            {
                MessageBox.Show("Ilość sztuk musi być liczbą.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (LaptopsListView.SelectedItem is Laptop selectedLaptop)
            {
                var result = MessageBox.Show(
                    $"Czy na pewno chcesz usunąć laptopa {selectedLaptop.NumerSeryjny}?",
                    "Potwierdzenie usunięcia",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    SQLiteDataAccess.UsunLaptop(selectedLaptop.NumerSeryjny);
                    ZaladujLaptopy();
                }
            }
            else
            {
                MessageBox.Show("Najpierw wybierz laptopa do usunięcia.", "Brak wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (LaptopsListView.SelectedItem is Laptop selectedLaptop)
            {
                UstawTrybEdycji(selectedLaptop);
            }
            else
            {
                MessageBox.Show("Najpierw wybierz laptopa do edycji.", "Brak wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            WyczyscFormularz();
            UstawTrybDodawania();
        }

        private void WyczyscFormularz()
        {
            SerialNumberTextBox.Clear();
            MarkaTextBox.Clear();
            ModelTextBox.Clear();
            OSComboBox.SelectedIndex = -1;
            QuantityTextBox.Clear();
        }


  
    }
}