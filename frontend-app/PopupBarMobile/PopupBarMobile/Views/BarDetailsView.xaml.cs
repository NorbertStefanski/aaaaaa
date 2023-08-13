using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Models;
using PopupBarMobile.Services.Data;
using PopupBarMobile.ViewModels;
using System.Text;

namespace PopupBarMobile.Views;

public partial class BarDetailsView : ContentPage
{
    private readonly Bar _bar; 

    public BarDetailsView(Bar item)
    {
        InitializeComponent();

        Title = item.Name;
        _bar = item;

        var viewModel = new BarDetailsViewModel(item);
        BindingContext = viewModel;
    }

    private void OnAddButtonClicked(object sender, EventArgs args)
    {
        var button = sender as Button;
        var cocktail = button.BindingContext as BarMenuItem;
        if (cocktail != null)
        {
            var viewModel = BindingContext as BarDetailsViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedCocktails.Add(cocktail);
            }
        }
    }

    private void OnGoToCheckoutButtonClicked(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        var viewModel = BindingContext as BarDetailsViewModel;
        var barId = _bar.Id;
        if (viewModel != null)
        {
            IOrderDataService orderDataService = new OrderDataService();
            Navigation.PushAsync(new CheckoutView(orderDataService, viewModel.SelectedCocktails, barId));
        }
    }

    private void OnCocktailSelected(object sender, SelectedItemChangedEventArgs args)
    {
        var cocktail = args.SelectedItem as BarMenuItem;
        if (cocktail == null)
            return;

        //cocktail.ImageUrl = "https://source.unsplash.com/random";
        Navigation.PushAsync(new CocktailDetailsView(cocktail));
        myListView.SelectedItem = null;
    }

    private void OnSubtractButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var cocktail = button.BindingContext as BarMenuItem;
        if (cocktail != null)
        {
            var viewModel = BindingContext as BarDetailsViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedCocktails.Remove(cocktail);
            }
        }
    }
}
