using Citas.Base;
using Citas.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Citas.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReadCitaList : Page
    {
        ObservableCollection<Cita> listaCita = new ObservableCollection<Cita>();

        public ReadCitaList()
        {
            this.InitializeComponent();
            this.Loaded += cargarLista;
        }

        private void cargarLista(object sender, RoutedEventArgs e)
        {
            LeerDatos citas = new LeerDatos();
            listaCita = citas.obtenerCitas();
            if (listaCita.Count == 0)
            {

                new DataBaseHelper().InsertCita(new Cita(0,"","12/07/2016","8:00"));
                listaCita = citas.obtenerCitas();
                Beliminar.IsEnabled = true;
            }
            lista.ItemsSource = listaCita.OrderByDescending(i => i.id_medico).ToList();
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void agregarContacto(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AgregarCita));
        }

        private async void borrarTodo(object sender, RoutedEventArgs e)
        {
            var ventana = new MessageDialog("Seguro que deseas eliminar todo?");
            ventana.Commands.Add(new UICommand("No", new UICommandInvokedHandler(Command)));
            ventana.Commands.Add(new UICommand("Si", new UICommandInvokedHandler(Command)));
            await ventana.ShowAsync();
        }

        private void Command(IUICommand command)
        {
            if (command.Label.Equals("Si"))
            {
                DataBaseHelper db = new DataBaseHelper();
                db.BorrarTodo();
                listaCita.Clear();
                Beliminar.IsEnabled = false;
                lista.ItemsSource = listaCita;
            }
        }

        private void seleccion(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
