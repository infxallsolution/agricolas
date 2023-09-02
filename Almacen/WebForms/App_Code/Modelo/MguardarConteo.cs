using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Modelo
{
    public class MguardarConteo
    {
        public MguardarConteo()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public string item { get; set; }
        public string consecutivo { get; set; }
        public string conteo { get; set; }
        public string noConteo { get; set; }
        public int empresa { get; set; }
        public string usuario { get; set; }
    }
}