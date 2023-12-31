using ContactAppXamarin.Helpers;
using ContactAppXamarin.Model;
using ContactAppXamarin.ViewModel;
using Xamarin.Forms;

namespace ContactAppXamarin
{
    public partial class NewPage : ContentPage
    {

        public NewPage(ContactoModel contactoModel)
        {
            InitializeComponent();
            DependencyService.Get<IPickPhotoService>();
            BindingContext = new ContactViewModel(contactoModel, Navigation);
        }

       

    }
}
