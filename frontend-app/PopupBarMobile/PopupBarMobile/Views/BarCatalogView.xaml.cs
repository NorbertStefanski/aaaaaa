using PopupBarMobile.Models;
using PopupBarMobile.Services.Data;
using PopupBarMobile.ViewModels;

namespace PopupBarMobile.Views;

public partial class BarCatalogView : ContentPage
{
	public BarCatalogView()
	{
		InitializeComponent();
        BindingContext = new BarCatalogViewModel(new BarDataService());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as BarCatalogViewModel;
        viewModel?.LoadBarsCommand.Execute(null);
    }

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        var item = e.Item as Bar;
        await Navigation.PushAsync(new BarDetailsView(item));
    }
}