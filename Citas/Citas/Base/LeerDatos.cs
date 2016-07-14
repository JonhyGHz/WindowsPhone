using Citas.Base;
using Citas.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citas.Base
{
    class LeerDatos
    {
        DataBaseHelper databaseFullCrack = new DataBaseHelper();
        public ObservableCollection<Cita> obtenerCitas()
        {
            return databaseFullCrack.leerCitas();
        }
    }
}
