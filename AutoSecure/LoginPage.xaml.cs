using AutoSecure.VistaModelo;
using Microsoft.Maui.Controls;
namespace AutoSecure;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new NavigationPage(new RegisterPage()));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new NavigationPage(new MainPage()));
    }
}