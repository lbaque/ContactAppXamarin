using ContactAppXamarin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppXamarin.ViewModel
{
    public class Locator
    {
        private static LoginViewModel _loginViewModel;

        public static void Initialize()
        {
            // Registra los ViewModels que necesites aquí
            _loginViewModel = new LoginViewModel();
        }

        public static LoginViewModel LoginViewModel => _loginViewModel;
    }
}
