using AppCrudXamarin.Base;
using AppCrudXamarin.Models;
using AppCrudXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCrudXamarin.ViewModels
{
    public class DepartamentoNewViewModel: ViewModelBase
    {
        private ServiceApiDepartamentos service;
        public DepartamentoNewViewModel(ServiceApiDepartamentos service)
        {
            this.service = service;
            this.Departamento = new Departamento();
        }

        private Departamento _Departamento;
        public Departamento Departamento
        {
            get { return this._Departamento; }
            set
            {
                this._Departamento = value;
                OnPropertyChanged("Departamento");
            }
        }

        public Command InsertarDepartamento
        {
           get
            {
                return new Command(async () =>
                {
                    await this.service.InsertDepartamento
                    (this.Departamento.Nombre, this.Departamento.Localidad);
                    MessagingCenter.Send<DepartamentosListViewModel>
                    (App.ServiceLocator.DepartamentosListViewModel, "RELOAD");
                    await Application.Current.MainPage
                    .DisplayAlert("Alert", "Insertado", "Ok");
                });
            }
        }
    }
}
