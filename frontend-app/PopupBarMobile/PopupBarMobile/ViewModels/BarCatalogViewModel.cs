using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.ViewModels
{
    public class BarCatalogViewModel
    {
        private readonly IBarDataService _barDataService;

        public BarCatalogViewModel(IBarDataService barDataService)
        {
            _barDataService= barDataService;
            LoadBarsCommand = new Command(async () => await LoadBarsAsync());
        }

        public ObservableCollection<Bar> Bars { get; set; } = new ObservableCollection<Bar>();

        public Command LoadBarsCommand { get; }

        public async Task LoadBarsAsync()
        {
            var bars = await _barDataService.GetBarsAsync();
            Bars.Clear();
            foreach (var bar in bars)
            {
                Bars.Add(bar);
            }
        }
    }
}
