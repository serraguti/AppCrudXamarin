using AppCrudXamarin.Base;
using AppCrudXamarin.Models;
using AppCrudXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCrudXamarin.ViewModels
{
    public class DepartamentoViewModel: ViewModelBase
    {
        private ServiceApiDepartamentos service;

        public DepartamentoViewModel(ServiceApiDepartamentos service)
        {
            this.service = service;
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

        public Command DeleteDepartamento
        {
            get
            {
                return new Command(async () =>
                {
                    await this.service.DeleteDepartamento
                    (this.Departamento.IdDepartamento);
                    await Application.Current.MainPage
                    .Navigation.PopModalAsync();
                });
            }
        }
    }
}
