using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Liquidacion
{
    public class CtransaccionVacaciones
    {
        #region Atributos
        private string codConcepto, concepto, signo;

        public string Signo
        {
            get { return signo; }
            set { signo = value; }
        }

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }

        public string CodConcepto
        {
            get { return codConcepto; }
            set { codConcepto = value; }
        }
        private decimal cantidad, valorTotal, valorUnitario;

        public decimal ValorUnitario
        {
            get { return valorUnitario; }
            set { valorUnitario = value; }
        }

        public decimal ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        #endregion Atributos

        public CtransaccionVacaciones()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public CtransaccionVacaciones(string codConcepto, string concepto, decimal cantidad, string signo, decimal valorTotal, decimal valorUnitario)
        {
            this.codConcepto = codConcepto;
            this.concepto = concepto;
            this.cantidad = cantidad;
            this.signo = signo;
            this.valorTotal = valorTotal;
            this.valorUnitario = valorUnitario;
        }
    }
}