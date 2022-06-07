using AppCrudXamarin.ViewModels;
using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AppCrudXamarin.Services
{
    public class ServiceIoC
    {
        private IContainer container;

        public ServiceIoC()
        {
            this.RegisterDependencies();
        }
        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ServiceApiDepartamentos>();
            //VIEWMODELS
            builder.RegisterType<DepartamentosListViewModel>();
            builder.RegisterType<DepartamentoViewModel>();
            string resourceName = "AppCrudXamarin.appsettings.json";
            Stream stream =
                GetType().GetTypeInfo().Assembly
                .GetManifestResourceStream(resourceName);
            IConfiguration configuration =
                new ConfigurationBuilder().AddJsonStream(stream)
                .Build();
            builder.Register<IConfiguration>(z => configuration);
            this.container = builder.Build();
        }

        public DepartamentoViewModel DepartamentoViewModel
        {
            get
            {
                return this.container.Resolve<DepartamentoViewModel>();
            }
        }

        public DepartamentosListViewModel DepartamentosListViewModel
        {
            get
            {
                return this.container.Resolve<DepartamentosListViewModel>();
            }
        }
    }
}
