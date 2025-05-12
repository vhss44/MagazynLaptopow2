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
using System.ComponentModel;
using Biblioteka;

namespace Magazyn
{
        
    public partial class EdycjaProduktu : Window
    {
        private Laptop EdytowanyLaptop;


        public EdycjaProduktu(Laptop laptop)
        {
            InitializeComponent();
            EdytowanyLaptop = laptop;
            SerialNumberTextBox.Text = laptop.NumerSeryjny;
            MarkaTextBox.Text = laptop.Marka;
            ModelTextBox.Text = laptop.Model;
            OSComboBox.SelectedItem = OSComboBox.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == laptop.SystemOperacyjny);
            QuantityTextBox.Text = laptop.IloscSztuk.ToString();

          
        }
        public bool SprawdzPoprawnoscDanych()
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


        public void ZapiszProdukt_Click(object sender, RoutedEventArgs e)
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

            SQLiteDataAccess.AktualizujLaptop(EdytowanyLaptop.NumerSeryjny, laptop);
            MessageBox.Show("Laptop zaktualizowany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }


    

        
    }
}
