using ContactAppXamarin.Database;
using ContactAppXamarin.Model;
using ContactAppXamarin.View;
using System;
using System.IO;
using Xamarin.Forms;

namespace ContactAppXamarin
{
    public partial class FetchPage : ContentPage
    {
        private ContactoDatabase database;
        public FetchPage()
        {
            InitializeComponent();
            database = new ContactoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactAppXamarin.db3"));
            
            var agregarButton = new ToolbarItem
            {
                Icon = "add_icon.png",
                Text = "+"
            };            
            agregarButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new NewPage(new ContactoModel()));
            };            
            ToolbarItems.Add(agregarButton);

            Titulo.Text = $"Welcome to Xamarin.Forms! {(Application.Current.Properties["User"] as string)}";
    
        }

 
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var contacts = await database.GetAllAsync(x=> !x.Deleted);
            ListContact.ItemsSource = contacts;
        }

        private async void ListContact_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return; // No hay selección válida
            var itemSeleccionado = (ContactoModel)e.SelectedItem;
            ListContact.SelectedItem = null;
            await Navigation.PushAsync(new DetailPage(itemSeleccionado));
            
        }

        private async void CerrarSesion_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Remove("User");
            Application.Current.Properties.Remove("UserId");

            await Navigation.PushAsync(new LoginPage());
        }
    }
}
