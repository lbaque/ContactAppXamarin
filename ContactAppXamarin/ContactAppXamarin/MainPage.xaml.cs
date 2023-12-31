using ContactAppXamarin.Database;
using ContactAppXamarin.Model;
using System;
using System.IO;
using Xamarin.Forms;

namespace ContactAppXamarin
{
    public partial class MainPage : ContentPage
    {
        private ContactoDatabase database;
        public MainPage()
        {
            InitializeComponent();
            database = new ContactoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactAppXamarin.db3"));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //var contacts = await database.FirstOrDefaultAsync(x=>x.Master == true);
            //if (contacts != null)
            //{
            //    BtnIngresar.IsVisible = false;
            //    BtnIniciar.IsVisible = true;
            //}
            //else
            //{
            //    BtnIngresar.IsVisible = true;
            //    BtnIniciar.IsVisible = false;
            //}


        }

        private async void RegistroButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPage(new ContactoModel()));
        }

        private async void BtnIniciar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FetchPage());
        }
    }
}
