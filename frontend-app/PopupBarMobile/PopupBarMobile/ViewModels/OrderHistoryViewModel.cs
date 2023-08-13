using Newtonsoft.Json;
using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PopupBarMobile.ViewModels
{
    public class OrderHistoryViewModel : INotifyPropertyChanged
    {
        private List<OrderSummary> _orders;
        public List<OrderSummary> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OrderHistoryViewModel()
        {
            // Get the orders from the service
            GetOrdersFromService();
        }

        private async void GetOrdersFromService()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync("http://10.0.2.2:5000/orders/" + Preferences.Get("UserId", string.Empty));
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var orders = JsonConvert.DeserializeObject<List<OrderSummary>>(json);
                        if (orders != null)
                        {
                            Orders = orders;
                        }
                        else
                        {
                            // Handle empty response
                            Orders = new List<OrderSummary>();
                        }
                    }
                    else
                    {
                        // Handle error
                        Orders = new List<OrderSummary>();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    await Application.Current.MainPage.DisplayAlert("Tip!", "Please log in to check your order history!", "OK");
                    Orders = new List<OrderSummary>();
                }
            }
        }


        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new Command(() =>
                {
                    GetOrdersFromService();
                }));
            }
        }
    }
}
