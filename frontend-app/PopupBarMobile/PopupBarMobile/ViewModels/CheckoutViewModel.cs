using Polly.Caching;
using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PopupBarMobile.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        private readonly IOrderDataService _orderDataService;
        private readonly string _barId; 

        public CheckoutViewModel(IOrderDataService orderDataService, ObservableCollection<BarMenuItem> selectedCocktails, string barId)
        {
            _orderDataService = orderDataService;
            _barId = barId;
            SelectedCocktails = selectedCocktails;

            ConfirmOrderCommand = new Command(async () => await ConfirmOrderAsync());
        }

        public ObservableCollection<BarMenuItem> SelectedCocktails { get; set; }

        public ICommand ConfirmOrderCommand { get; set; }

        public bool ApplyDiscount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _tableId;
        public int TableId
        {
            get => _tableId;
            set
            {
                if (_tableId != value)
                {
                    _tableId = value;
                    OnPropertyChanged();
                }
            }
        }

        public double OrderTotal
        {
            get
            {
                double total = SelectedCocktails.Sum(item => item.Price);
                if (Preferences.ContainsKey("UserId"))
                {
                    ApplyDiscount = true;
                    OnPropertyChanged(nameof(ApplyDiscount));
                    var discount = total * 0.1;
                    total -= discount;
                }
                return total;
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ConfirmOrderAsync()
        {
            int? userId = Preferences.ContainsKey("UserId") ? Convert.ToInt32(Preferences.Get("UserId", string.Empty)) : (int?)null;
            var order = new Order(_barId, TableId, userId, CalculateOrderTotal());
            foreach (var item in SelectedCocktails)
            {
                order.AddOrderedItem(item.Id);
            }
            await _orderDataService.PostOrderAsync(order);
            await Application.Current.MainPage.DisplayAlert("Order Confirmed", "Your order has been placed.", "OK");
        }

        private double CalculateOrderTotal()
        {
            var total = 0.00;
            foreach (var item in SelectedCocktails)
            {
                total += item.Price; 
            }

            if (Preferences.ContainsKey("UserId"))
            {
                var discount = total * 0.1; 
                total -= discount;
            }

            return total; 
        }
    }
}
