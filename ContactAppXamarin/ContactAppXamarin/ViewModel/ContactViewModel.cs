using ContactAppXamarin.Database;
using ContactAppXamarin.FluentValidation;
using ContactAppXamarin.Helpers;
using ContactAppXamarin.Model;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactAppXamarin.ViewModel
{
    public class ContactViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private ContactModelValidator _validator;
        private ContactoDatabase _database;


        public ContactoModel Contacto { get; set; }

        private byte[] _foto;

        public byte[] Foto
        {
            get { return _foto; }
            set
            {
                if (_foto != value)
                {
                    _foto = value;
                    OnPropertyChanged(nameof(Foto));
                }

            }
        }




        public Command GuardarCommand { get; }
        public Command CargarFotoCommand { get; }
        private readonly INavigation _navigation;
        public ContactViewModel(ContactoModel contacto, INavigation navigation)
        {
            _navigation = navigation;
            Contacto = contacto;
            Foto = contacto.Foto;
            GuardarCommand = new Command(Guardar);
            CargarFotoCommand = new Command(async () => await CargarFotoAsync());
            _validator = new ContactModelValidator();
            _database = new ContactoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactAppXamarin.db3"));
        }

        private async void Guardar()
        {
            var validationResult = _validator.Validate(Contacto);

            if (validationResult.IsValid)
            {
                var existe = await _database.FirstOrDefaultAsync(x => x.Id == Contacto.Id);
                Contacto.synchronized = false;

                if (existe is null)
                {                    
                    Contacto.UsuarioId = new Guid((Application.Current.Properties["UserId"] as string));
                    await _database.InsertAsync(Contacto);
                }
                else
                    await _database.UpdateAsync(Contacto);

                await _navigation.PopAsync();
            }
            else
            {
                // Mostrar los mensajes de error de validación
                string errores = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));
                DisplayAlert("Error de Validación", errores, "OK");
            }
        }

        private async Task CargarFotoAsync()
        {
            try
            {
                // Simular la carga de la fotografía desde la galería o la cámara
                var result = await DependencyService.Get<IPickPhotoService>().GetImageStreamAsync();

                if (result != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        result.CopyTo(ms);
                        Contacto.Foto = ms.ToArray();
                        Foto = ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción en la carga de la foto
                Console.WriteLine("Error al cargar la foto: " + ex.Message);
            }
        }

        private void DisplayAlert(string titulo, string mensaje, string cancel)
        {
            Application.Current.MainPage.DisplayAlert(titulo, mensaje, cancel);
        }
    }
}
