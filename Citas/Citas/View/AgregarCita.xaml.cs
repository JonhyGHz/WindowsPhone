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
    
    public sealed partial class AgregarCita : Page
    {
        ObservableCollection<Medico> listaMedico = new ObservableCollection<Medico>();
        string id_cita = "";
        public AgregarCita()
        {
            this.InitializeComponent();
            cargarMedicos();

            id_cita = System.Convert.ToString(new DataBaseHelper().idCita().id + 1);

        }
        private void cargarMedicos()
        {
            DataBaseHelper db = new DataBaseHelper();
            listaMedico = db.leerMedicos();

            if(listaMedico.Count == 0)
            {
                db.agregarMedico(new Medico("Dr. Alexander","Fleming",""));
                db.agregarMedico(new Medico("Dr. Joan", "Friedrich", "Miescher"));
                db.agregarMedico(new Medico("Dr. Edward", "Jenner", ""));
                db.agregarMedico(new Medico("Dr. Karla", "Landsteiner", ""));
                db.agregarMedico(new Medico("Dr. Rene", "Favaloro", ""));
                db.agregarMedico(new Medico("Dr. Rene", "Vertiz", "Barruecos"));

                listaMedico = db.leerMedicos();
            }
            listMedico.ItemsSource = listaMedico.OrderByDescending(i => i.id).ToList();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        
        private void TxbCambio(object sender, RoutedEventArgs e)
        {

        }


        private async void agregarCita(object sender, RoutedEventArgs e)
        {

            string fecha = fechaPicker.Date.ToString();
            string hora = tiempoPicker.Time.ToString();
            if(txtNombre.Text != "")
            {
                if (listMedico.SelectedIndex != -1)
                {
                    int idCita = Int32.Parse(id_cita);
                    Medico item = listMedico.SelectedItem as Medico;
                    string nombre = item.nombre.ToString();
                    new DataBaseHelper().InsertCita(new Cita(idCita,txtNombre.Text+" - "+nombre, fecha, hora));
                    Windows.UI.Popups.MessageDialog messageDialog = new Windows.UI.Popups.MessageDialog("Cita registrada");
                    await messageDialog.ShowAsync();
                    Frame.Navigate(typeof(ReadCitaList));
                }
                else
                {
                    Windows.UI.Popups.MessageDialog messageDialog = new Windows.UI.Popups.MessageDialog("Seleccione un medico!");
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                Windows.UI.Popups.MessageDialog messageDialog = new Windows.UI.Popups.MessageDialog("Introduzca un nombre!");
                await messageDialog.ShowAsync();
            }
            

        }

        private async void listMedicoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
