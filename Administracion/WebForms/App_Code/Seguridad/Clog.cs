using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Seguridad
{
    public class Clog
    {
        public Clog()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView GetLogFechaParametro(DateTime fechaI, DateTime fechaF, string parametro, int empresa)
        {
            string[] iParametros = new string[] { "@fechaI", "@fechaF", "@empresa" };
            object[] objValores = new object[] { fechaI, fechaF, empresa };

            DataView dvLog = Cacceso.DataSetParametros(
                "spSeleccionaLogFechaParametro",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;

            dvLog.RowFilter = "usuario like '%" + parametro +
                "%' or descripcion like '%" + parametro +
                "%' or entidad like '%" + parametro +
                "%' or estado like '%" + parametro +
                "%' or mensajeSistema like '%" + parametro + "%'";

            return dvLog;
        }
    }
}