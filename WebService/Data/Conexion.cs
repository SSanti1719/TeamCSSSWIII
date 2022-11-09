using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Data
{
    public class Conexion
    {

        public string rutaConexion = @"Data Source =.\SQLEXPRESS; Initial Catalog = DBPRUEBAS; User ID = consensus; Password = 21jejAlo";

        public string datosConexion()
        {
            return this.rutaConexion;
        }
    }
}
