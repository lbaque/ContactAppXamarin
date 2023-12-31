using ContactAppXamarin.FluentValidation;
using ContactAppXamarin.Helpers;
using ContactAppXamarin.Model;
using ContactAppXamarin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactAppXamarin.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private UsuarioModelValidator _validator;

        private UsuarioModel _usuario;

        public UsuarioModel Usuario
        {
            get { return _usuario; }
            set { 
                
                if (_usuario != value) {
                    _usuario = value;
                    OnPropertyChanged(nameof(Usuario));
                }

            }
        }

        public  LoginViewModel()
        {
            Usuario = new UsuarioModel();
            _validator = new UsuarioModelValidator();
        }

        public ICommand LoginCommand => new Command(OnLoginClicked);
        public ICommand RegisterCommand => new Command(OnRegisterClicked);

        private async void OnLoginClicked()
        {
            try
            {
                var validationResult = _validator.Validate(Usuario);

                if (validationResult.IsValid)
                {
                    string password = sha256(Usuario.Password);

                    var http = new HttpClientUris().ContactApi();
                    string uri = ServiceRoute.Contact.V1.Usuario.Fetch;
                    uri = $"{uri}?User={Usuario.User}";
                    var data = await http.GetFromJsonAsync<List<UsuarioModel>>(uri);
                    if (data.Count == 0)
                        throw new Exception("Error usuario no existe");
                    else
                    {
                        var usuario = data.FirstOrDefault();

                        if (usuario.Password.ToUpper() == password.ToUpper())
                        {
                            Application.Current.Properties["User"] = usuario.User;
                            Application.Current.Properties["UserId"] = usuario.Id.ToString();
                            await Application.Current.SavePropertiesAsync();

                            await App.Current.MainPage.Navigation.PushAsync(new FetchPage());
                        }
                        else
                            throw new Exception("Verifique sus credenciales");
                    }
                        
                }
                else
                {
                    // Mostrar los mensajes de error de validación
                    string errores = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));
                    DisplayAlert("Error de Validación", errores, "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error de Validación", ex.Message, "OK");
            }

            
        }
        private async void OnRegisterClicked() {
            await App.Current.MainPage.Navigation.PushAsync(new NewPage(new ContactoModel()));
        }

        private void DisplayAlert(string titulo, string mensaje, string cancel)
        {
            Application.Current.MainPage.DisplayAlert(titulo, mensaje, cancel);
        }

        private string sha256(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256Hash.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            }
        }
    }

    
}
