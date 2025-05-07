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
        public Laptop EdytowanyLaptop { get; private set; }
   

        public EdycjaProduktu(Laptop laptop)
        {
            InitializeComponent();
            EdytowanyLaptop = new Laptop
            {
                NumerSeryjny = laptop.NumerSeryjny,
                Marka = laptop.Marka,
                Model = laptop.Model,
                SystemOperacyjny = laptop.SystemOperacyjny,
                IloscSztuk = laptop.IloscSztuk
            };

            DataContext = this;
        }
    
        private void ZapiszProdukt_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SerialNumberTextBox.Clear();
            MarkaTextBox.Clear();
            ModelTextBox.Clear();
            OSComboBox.SelectedIndex = -1;
            QuantityTextBox.Clear();
        }
    }
}
