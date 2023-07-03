using AzureB2C.Mobile.MSALClient;
using AzureB2C.Mobile.Services;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Text;

namespace AzureB2C.Mobile
{
    public partial class MainPage : ContentPage
    {
        public IPublicClientApplication IdentityClient { get; set; }

        public MainPage()
        {
            InitializeComponent();
            IAccount cachedUserAccount = PublicClientSingleton.Instance.MSALClientHelper.FetchSignedInUserFromCache().Result;

            _ = Dispatcher.DispatchAsync(async () =>
            {
                if (cachedUserAccount == null)
                {
                    SignIn.IsEnabled = true;
                }
                else
                {
                    await Shell.Current.GoToAsync("scopeview");
                }
            });
        }

        private async void SignIn_Clicked(object sender, EventArgs e)
        {
            await PublicClientSingleton.Instance.AcquireTokenSilentAsync();
            await Shell.Current.GoToAsync("scopeview");
        }
        protected override bool OnBackButtonPressed() { return true; }
    }
}