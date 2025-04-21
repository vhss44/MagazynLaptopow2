using System;
using System.Collections.Generic;
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

namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
