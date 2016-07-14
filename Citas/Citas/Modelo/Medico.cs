using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citas.Modelo
{
    class Medico
    {

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }


        public Medico()
        {

        }

        public Medico(string name, string apellidoP, string apellidoM)
        {
            nombre = name;
            apellidoPaterno = apellidoP;
            apellidoMaterno = apellidoM;
        }
    }
}
