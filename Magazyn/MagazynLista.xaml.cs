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
using System.Collections.ObjectModel;


namespace Magazyn
{




    public partial class MagazynLista : Window
    {
       

        private ObservableCollection<Laptop> laptopy = new ObservableCollection<Laptop>();
        private ICollectionView view;


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
            laptopy = new ObservableCollection<Laptop>(SQLiteDataAccess.ZaladujLaptopy());
            view = CollectionViewSource.GetDefaultView(laptopy);
            view.Filter = FilterByNumerSeryjny;
            LaptopsListView.ItemsSource = view;
        }

        private void LaptopySortowanie(object sender, DataGridSortingEventArgs e)
        {
            var column = e.Column;
            string propertyName = column.SortMemberPath;
            e.Handled = false;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            view.Refresh();
        }
        private bool FilterByNumerSeryjny(object obj)
        {
            if (obj is Laptop laptop)
            {
                return string.IsNullOrWhiteSpace(txtFilter.Text) ||
                       laptop.NumerSeryjny.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            return false;
        }

        private void LaptopsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LaptopsListView.SelectedItem is Laptop selectedLaptop)
            {


                var editWindow = new EdycjaProduktu(selectedLaptop);

                if (editWindow.ShowDialog() == true)
                {
                    ZaladujLaptopy();
                }

            }
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


                var editWindow = new EdycjaProduktu(selectedLaptop);

                if (editWindow.ShowDialog() == true)
                {
                    ZaladujLaptopy();
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
           
               var laptopy = SQLiteDataAccess.ZaladujLaptopy();
                LaptopsListView.ItemsSource = laptopy;
           
        }
    }
}


