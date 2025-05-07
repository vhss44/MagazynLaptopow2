using Biblioteka;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

    public partial class MagazynLista : Window
    {
        public MagazynLista()
        {
            InitializeComponent();
            ZaladujLaptopy();
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
        private void ZaladujLaptopy()
        {
      

            var laptopy = SQLiteDataAccess.ZaladujLaptopy();
            LaptopsListView.ItemsSource = laptopy;
        }
        private void LaptopySortowanie(object sender, DataGridSortingEventArgs e)
        {
            var column = e.Column;
            string propertyName = column.SortMemberPath;
            e.Handled = false; 
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                LaptopsListView.ItemsSource = originalLaptopyList;
                return;
            }

            var filtered = originalLaptopyList
                .Where(l => l.NumerSeryjny.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase) ||
                           l.Marka.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase) ||
                           l.Model.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase))
                .ToList();

            LaptopsListView.ItemsSource = filtered;
        }
    }

 }


