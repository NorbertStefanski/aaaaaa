using PopupBarMobile.Contracts.Repository;
using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Repository;
using PopupBarMobile.Services;
using PopupBarMobile.Services.Data;
using PopupBarMobile.Services.Identity;
using PopupBarMobile.Settings;
using PopupBarMobile.ViewModels;
using PopupBarMobile.Views;

namespace PopupBarMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        RegisterDependencies(builder.Services);

        return builder.Build();
    }

    private static void RegisterDependencies(IServiceCollection services)
    {
        //Views
        services.AddTransient<BarDetailsView>();
        services.AddTransient<BarCatalogView>();
        services.AddTransient<CheckoutView>();
        services.AddTransient<CocktailDetailsView>();
        services.AddTransient<OrderHistoryView>();
        services.AddTransient<LoginView>();

        //Viewmodels
        services.AddTransient<BarCatalogViewModel>();
        services.AddTransient<BarDetailsViewModel>();
        services.AddTransient<CheckoutViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<OrderHistoryViewModel>();

        //Services 
        services.AddTransient<IBarDataService, BarDataService>();
        services.AddTransient<IOrderDataService, OrderDataService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<INavigationService, NavigationService>();

        //General
        services.AddSingleton<IGenericRepository, GenericRepository>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddSingleton<IAppSettings, DevAppSettings>();
    }
}
