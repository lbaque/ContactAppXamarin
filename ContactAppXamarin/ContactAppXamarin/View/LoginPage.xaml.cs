using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactAppXamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // Se ejecuta cuando la página se muestra
            // Aquí puedes realizar acciones como cargar datos o realizar actualizaciones

            if (Application.Current.Properties.ContainsKey("User"))
            {
                if ((Application.Current.Properties["User"] as string).Length > 0)
                {
                    await Navigation.PushAsync(new FetchPage());
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
           
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            
        }

    }
}