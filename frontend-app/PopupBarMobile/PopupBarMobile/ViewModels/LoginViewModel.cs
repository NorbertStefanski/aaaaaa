using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls;
using PopupBarMobile.Services;
using PopupBarMobile.Services.Identity;
using PopupBarMobile.ViewModels.Base;
using PopupBarMobile.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace PopupBarMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenProvider _tokenProvider;

        public bool IsLoggedIn { get; set; } = true; 
        public bool IsBusy { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        public LoginViewModel(IIdentityService identityService, ITokenProvider tokenProvider, INavigationService navigationService)
        {
            _identityService = identityService;
            _tokenProvider = tokenProvider;

            LoginCommand = new Command(OnLoginCommandExecuted, CanLoginCommandExecute);
            LogoutCommand = new Command(OnLogoutCommandExecuted, CanLogoutCommandExecute);
        }

        private async void OnLoginCommandExecuted()
        {
            IsBusy = true;

            ILoginResult loginResult = await _identityService.LoginAsync();

            if (!loginResult.IsError)
            {
                _tokenProvider.AuthAccessToken = loginResult.AccessToken;
                //await Application.Current.MainPage.DisplayAlert("Login Success", "You are logged in, enjoy 10% discount on everything!.", "OK");

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(loginResult.AccessToken);
                var userId = token.Claims.First(c => c.Type == "sub").Value;
                Preferences.Set("UserId", userId);

                await Application.Current.MainPage.DisplayAlert("Login Success", "You are logged in, enjoy 10% discount on everything!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error!", "Something went wrong, please try again!", "OK");
            }

            IsBusy = false;
        }

        private bool CanLoginCommandExecute()
        {
            return !IsBusy;
        }

        private void OnLogoutCommandExecuted()
        {
            Preferences.Clear();
        }

        private bool CanLogoutCommandExecute()
        {
            return true;
        }
    }
}
