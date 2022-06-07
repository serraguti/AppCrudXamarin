﻿using AppCrudXamarin.Base;
using AppCrudXamarin.Models;
using AppCrudXamarin.Services;
using AppCrudXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCrudXamarin.ViewModels
{
    public class DepartamentosListViewModel: ViewModelBase
    {
        private ServiceApiDepartamentos service;

        public DepartamentosListViewModel(ServiceApiDepartamentos service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadDepartamentosAsync();
            });
        }

        private async Task LoadDepartamentosAsync()
        {
            List<Departamento> departamentos =
                await this.service.GetDepartamentosAsync();
            this.Departamentos =
                new ObservableCollection<Departamento>(departamentos);
        }

        private ObservableCollection<Departamento> _Departamentos;

        public ObservableCollection<Departamento> Departamentos
        {
            get { return this._Departamentos; }
            set
            {
                this._Departamentos = value;
                OnPropertyChanged("Departamentos");
            }
        }

        private Departamento _DepartamentoSeleccionado;

        public Departamento DepartamentoSeleccionado
        {
            get { return this._DepartamentoSeleccionado; }
            set
            {
                this._DepartamentoSeleccionado = value;
                OnPropertyChanged("DepartamentoSeleccionado");
            }
        }

        public Command ShowDetails
        {
            get
            {
                return new Command(async (departamento) =>
                {
                    Departamento dept = departamento as Departamento;
                    DepartamentoView view = new DepartamentoView();
                    //RECUPERAMOS NUESTRO VIEWMODEL
                    DepartamentoViewModel viewmodel =
                    App.ServiceLocator.DepartamentoViewModel;
                    //INDICAMOS EL OBJETO A ENLAZAR EN VIEWMODEL
                    viewmodel.Departamento = dept;
                    //ENLAZAMOS LA VISTA CON SU VIEWMODEL
                    view.BindingContext = viewmodel;
                    await
                    Application.Current.MainPage
                    .Navigation.PushModalAsync(view);
                });
            }
        }

        public Command DeleteDepartamento
        {
            get
            {
                return new Command(async (id) =>
                {

                });
            }
        }
    }
}
