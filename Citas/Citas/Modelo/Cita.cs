using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citas.Modelo
{
    class Cita
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id { get; set; }
        public int id_medico { get; set; }
        public string nombre { get; set; }
        public string fecha_registro { get; set; }
        public string fecha_cita { get; set; }
        public string hora { get; set; }

        public Cita()
        {

        }

        public Cita(int idMedico,string name, string fechaCita, string hora_cita)
        {
            id_medico = idMedico;
            nombre = name;
            fecha_registro = DateTime.Now.ToString();
            fecha_cita = fechaCita;
            hora = hora_cita;
        }
    }
}
