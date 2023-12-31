using ContactAppXamarin.Database;
using ContactAppXamarin.Model;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactAppXamarin.ViewModel
{
    public class DetailViewModel : INotifyPropertyChanged
    {   
       
        public ICommand EditarCommand => new Command(OnEditarClicked);
        public ICommand EliminarCommand => new Command(OnEliminarClicked);

        private ContactoModel _contacto;

        public ContactoModel Contacto
        {
            get { return _contacto; }
            set {
                if (_contacto != value)
                {
                    _contacto = value;
                    OnPropertyChanged(nameof(Contacto));
                }
            }
        }

        private ContactoDatabase _database;
        private INavigation _navigation;
        public DetailViewModel(ContactoModel contactoModel, INavigation navigation)
        {
            Contacto = contactoModel;
            _navigation = navigation;
            _database = new ContactoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactAppXamarin.db3")); ;
        }

        // Implementación de métodos para las acciones de los comandos
        private async void OnEditarClicked()
        {
            await _navigation.PushAsync(new NewPage(_contacto));
        }

        private async void OnEliminarClicked()
        {
            await _database.DeleteAsync(_contacto.Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
