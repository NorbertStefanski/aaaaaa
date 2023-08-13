using PopupBarMobile.Services.Data;
using PopupBarMobile.ViewModels;
using System.Globalization;

namespace PopupBarMobile.Views;

public partial class OrderHistoryView : ContentPage
{
    private readonly OrderHistoryViewModel _viewModel;
    public OrderHistoryView()
    {
        InitializeComponent();

        var culture = new CultureInfo("fr-FR");
        culture.NumberFormat.CurrencySymbol = "€";
        Thread.CurrentThread.CurrentCulture = culture;

        _viewModel = new OrderHistoryViewModel();
        BindingContext = _viewModel;
    }
}