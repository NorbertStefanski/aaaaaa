using PopupBarMobile.Models;

namespace PopupBarMobile.Views;

public partial class CocktailDetailsView : ContentPage
{
    public CocktailDetailsView(BarMenuItem cocktail)
    {
        InitializeComponent();
        BindingContext = cocktail;
    }
}

