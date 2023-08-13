using Newtonsoft.Json;
using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Models;
using PopupBarMobile.ViewModels;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PopupBarMobile.Views;

public partial class CheckoutView : ContentPage
{
    private readonly CheckoutViewModel _viewModel;

    public CheckoutView(IOrderDataService orderDataService, ObservableCollection<BarMenuItem> selectedCocktails, string barId)
    {
        InitializeComponent();

        var culture = new CultureInfo("fr-FR");
        culture.NumberFormat.CurrencySymbol = "€";
        Thread.CurrentThread.CurrentCulture = culture;

        _viewModel = new CheckoutViewModel(orderDataService, selectedCocktails, barId);
        SelectedCocktails = selectedCocktails;
        BindingContext = _viewModel;
    }

    public ObservableCollection<BarMenuItem> SelectedCocktails { get; set; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
