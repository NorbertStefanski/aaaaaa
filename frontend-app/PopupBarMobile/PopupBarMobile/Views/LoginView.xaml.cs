using PopupBarMobile.ViewModels;

namespace PopupBarMobile.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}