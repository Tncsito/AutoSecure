using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoSecure.MongoDB;
using Microsoft.Maui.Controls;
using MongoDB.Driver;

namespace AutoSecure.VistaModelo
{
    public class RegisterViewModel : BindableObject
    {
        private string _email;
        private string _password;
        private string _confirmPassword;
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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            _mongoDBService = new MongoDBService();
            RegisterCommand = new Command(OnRegister);
        }

        private async void OnRegister()
        {
            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            var result = await _mongoDBService.RegisterUser(Email, Password);
            if (result)
            {
                await Application.Current.MainPage.DisplayAlert("Registro", "Registro exitoso", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error al registrar el usuario", "OK");
            }
        }
    }
}
