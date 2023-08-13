using PopupBarMobile.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PopupBarMobile.ViewModels
{
    public class BarDetailsViewModel : INotifyPropertyChanged
    {
        public Bar Bar { get; set; }
        public ObservableCollection<BarMenuItem> MenuItems { get; set; }
        public Command<BarMenuItem> CocktailDetailsCommand { get; set; }
        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        public BarDetailsViewModel(Bar bar)
        {
            Bar = bar;
            MenuItems = new ObservableCollection<BarMenuItem>(bar.barMenuItems ?? new List<BarMenuItem>());
            SelectedCocktails = new ObservableCollection<BarMenuItem>();

            CocktailDetailsCommand = new Command<BarMenuItem>(async (cocktail) =>
            {
                await Shell.Current.GoToAsync($"cocktaildetailsview?cocktail={cocktail}");
            });

 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private ObservableCollection<BarMenuItem> _selectedCocktails;
        public ObservableCollection<BarMenuItem> SelectedCocktails
        {
            get { return _selectedCocktails; }
            set
            {
                _selectedCocktails = value;
                OnPropertyChanged();
            }
        }
    }
}
