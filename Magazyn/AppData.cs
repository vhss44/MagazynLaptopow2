using Biblioteka;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Magazyn
{
    public class AppData : INotifyPropertyChanged
    {
        private ObservableCollection<Laptop> _laptopy;

        public ObservableCollection<Laptop> Laptopy
        {
            get => _laptopy;
            set
            {
                _laptopy = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}