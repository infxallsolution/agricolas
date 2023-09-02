using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.General
{
    public class Cusuario
    {

        public Cusuario()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string RetornaNombreUsuario(string id)
        {
            string[] iParametros = new string[] { "@id" };
            string[] oParametros = new string[] { "@nombre" };
            object[] objValores = new object[] { id };

            return Convert.ToString(
                Cacceso.ExecProc(
                    "spRetornaNombreUsuario",
                    iParametros,
                    oParametros,
                    objValores,
                    "seguridad").GetValue(0));
        }
    }
}