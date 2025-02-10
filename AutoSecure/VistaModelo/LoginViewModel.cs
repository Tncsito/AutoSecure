using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoSecure.MongoDB;
using Microsoft.Maui.Controls;

namespace AutoSecure.VistaModelo
{
    public class LoginViewModel : BindableObject
    {
        private string _email;
        private string _password;
        private readonly MongoDBService _mongoDBService;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _mongoDBService = new MongoDBService();
            LoginCommand = new Command(OnLogin);
        }

        private async void OnLogin()
        {
            var user = await _mongoDBService.AuthenticateUser(Email, Password);
            if (user != null)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Credenciales incorrectas", "OK");
            }
        }
    }
}