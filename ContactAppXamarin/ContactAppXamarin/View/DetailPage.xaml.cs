using ContactAppXamarin.Database;
using ContactAppXamarin.Model;
using ContactAppXamarin.ViewModel;
using System;
using System.IO;
using Xamarin.Forms;

namespace ContactAppXamarin
{
    public partial class DetailPage : ContentPage
    {
        

        public DetailPage(ContactoModel _contact)
        {
            InitializeComponent();
            BindingContext = new DetailViewModel(_contact,Navigation);
        }


        
    }
}
