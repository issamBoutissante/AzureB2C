using AzureB2C.Mobile.MSALClient;
using AzureB2C.Mobile.Services;
using Microsoft.Identity.Client;

namespace AzureB2C.Mobile;

public partial class ScopeView : ContentPage
{
    public IEnumerable<string> AccessTokenScopes { get; set; } = new string[] { "No scopes found in access token" };
    public ScopeView()
    {
        BindingContext = this;
        InitializeComponent();

        _ = SetViewDataAsync();
    }

    private async Task SetViewDataAsync()
    {
        try
        {
            _ = await PublicClientSingleton.Instance.AcquireTokenSilentAsync();

            ExpiresAt.Text = PublicClientSingleton.Instance.MSALClientHelper.AuthResult.ExpiresOn.ToLocalTime().ToString();
            AccessTokenScopes = PublicClientSingleton.Instance.MSALClientHelper.AuthResult.Scopes
                .Select(s => s.Split("/").Last());

            Scopes.ItemsSource = AccessTokenScopes;
            GetUserFromAPI();
        }

        catch (MsalUiRequiredException)
        {
            await Shell.Current.GoToAsync("scopeview");
        }
    }

    protected override bool OnBackButtonPressed() { return true; }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await PublicClientSingleton.Instance.SignOutAsync().ContinueWith((t) =>
        {
            return Task.CompletedTask;
        });

        await Shell.Current.GoToAsync("mainPage");
    }
    private async void GetUserFromAPI()
    {
        APIClient.Initialize(PublicClientSingleton.Instance.MSALClientHelper.AuthResult.AccessToken);
        string accessToken = PublicClientSingleton.Instance.MSALClientHelper.AuthResult.AccessToken;
        try
        {
            await APIClient.Client.UsersPOSTAsync(new User());
            var users = await APIClient.Client.UsersAllAsync();
            await Shell.Current.DisplayAlert("Alert", $"You have {users.Count()} users in database", "OK");

        }catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", ex.Message, "OK");
        }
    }
}