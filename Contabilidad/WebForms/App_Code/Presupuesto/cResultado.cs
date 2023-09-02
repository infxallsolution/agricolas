using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Presupuesto
{
    public class cResultado
    {
        private string variable;
        private string periocidad;
        private string tipotransaccion;
        private int mes;
        private string objeto;

        public string Variable
        {
            get
            {
                return variable;
            }

            set
            {
                variable = value;
            }
        }

        public string Periocidad
        {
            get
            {
                return periocidad;
            }

            set
            {
                periocidad = value;
            }
        }

        public string Tipotransaccion
        {
            get
            {
                return tipotransaccion;
            }

            set
            {
                tipotransaccion = value;
            }
        }

        public int Mes
        {
            get
            {
                return mes;
            }

            set
            {
                mes = value;
            }
        }

        public string Objeto
        {
            get
            {
                return objeto;
            }

            set
            {
                objeto = value;
            }
        }
    }
}