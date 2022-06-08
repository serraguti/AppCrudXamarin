using AppCrudXamarin.Code;
using AppCrudXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCrudXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainDepartamentosView : MasterDetailPage
    {
        public MainDepartamentosView()
        {
            InitializeComponent();
            ObservableCollection<MenuPageItem> menu =
                new ObservableCollection<MenuPageItem>
                {
new MenuPageItem{ 
    Titulo = "Departamentos List", Icono = "arriba.png"
    , TypePage=typeof(DepartamentosView)},
new MenuPageItem{
    Titulo = "Nuevo departamento", Icono = "abajo.png"
    , TypePage=typeof(DepartamentoNewView)},
new MenuPageItem{
    Titulo = "Collection View", Icono = "groot.png"
    , TypePage=typeof(DepartamentosCollectView)}
                };
            this.lsvMenu.ItemsSource = menu;
            Detail =
                new NavigationPage
                ((Page)Activator.CreateInstance(typeof(MainPage)));
            this.lsvMenu.ItemSelected += LsvMenu_ItemSelected;
        }

        private void LsvMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuPageItem item = e.SelectedItem as MenuPageItem;
            Detail =
                new NavigationPage((Page)Activator.CreateInstance(item.TypePage));
            IsPresented = false;
        }
    }
}