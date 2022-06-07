using AppCrudXamarin.Services;
using AppCrudXamarin.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCrudXamarin
{
    public partial class App : Application
    {
        private static ServiceIoC _ServiceLocator;

        public static ServiceIoC ServiceLocator
        {
            get
            {
                return _ServiceLocator = _ServiceLocator ?? new ServiceIoC();
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainDepartamentosView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
