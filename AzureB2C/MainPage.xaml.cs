using AzureB2C.Services;
using Microsoft.Identity.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AzureB2C;

public partial class MainPage : ContentPage
{

    public MainPage()
	{
		InitializeComponent();
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        var authService = new AuthService(); // most likely you will inject it in the constructor, but for simplicity let's initialize it here
        var result = await authService.LoginAsync(CancellationToken.None);
        var token = result?.IdToken; // you can also get AccessToken if you need it
        if (token != null)
        {
            var handler = new JwtSecurityTokenHandler();
            var data = handler.ReadJwtToken(token);
            var claims = data.Claims.ToList();
            if (data != null)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"Name: {data.Claims.FirstOrDefault(x => x.Type.Equals("name"))?.Value}");
                stringBuilder.AppendLine($"Email: {data.Claims.FirstOrDefault(x => x.Type.Equals("preferred_username"))?.Value}");
                await Shell.Current.DisplayAlert("Alert", stringBuilder.ToString(), "OK");
            }
        }

    }

    private void SignUp_Clicked(object sender, EventArgs e)
    {

    }
}

