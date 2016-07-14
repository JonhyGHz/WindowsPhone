using Citas.Modelo;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citas.Base
{
    class DataBaseHelper
    {
        SQLiteConnection dbConn;
        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!existe(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<Medico>();
                        dbConn.CreateTable<Cita>();
                    }
                }
                return true;
            }
            catch{return false;}
        }

        private async Task<bool> existe(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Retrieve the specific contact from the database. 
        public Cita leerCita(int id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Cita>("select * from Cita where id =" + id).FirstOrDefault();
                return existingconact;
            }
        }

        //Obtiene todas las citas 
        public ObservableCollection<Cita> leerCitas()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Cita> myCollection = dbConn.Table<Cita>().ToList<Cita>();
                ObservableCollection<Cita> CitaList = new ObservableCollection<Cita>(myCollection);
                return CitaList;
            }
        }
        // Inserta una cita en la tabla 
        public void InsertCita(Cita newcita)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(newcita);
                });
            }
        }

        //Leer todos los medicos
        public ObservableCollection<Medico> leerMedicos()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Medico> myCollection = dbConn.Table<Medico>().ToList<Medico>();
                ObservableCollection<Medico> CitaList = new ObservableCollection<Medico>(myCollection);
                return CitaList;
            }
        }

        // insertar nuevo medico
        public void agregarMedico(Medico medico)
        {
            using (var conexion = new SQLiteConnection(App.DB_PATH))
            {
                conexion.RunInTransaction(() =>
                {
                    conexion.Insert(medico);
                });
            }
        }

        public Cita idCita()
        {
            using (var conexion = new SQLiteConnection(App.DB_PATH))
            {
                var cita = conexion.Query<Cita>("select * from Cita where"+
                    " id =(select max(id) from Cita)").FirstOrDefault();
                if (cita != null)
                {
                    return cita;
                }
                return new Cita(1,"", "", "");
            }
        }

        public void BorrarTodo()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {

                dbConn.DropTable<Medico>();
                dbConn.CreateTable<Cita>();
                dbConn.Dispose();
                dbConn.Close();

            }
        }
    }
}
