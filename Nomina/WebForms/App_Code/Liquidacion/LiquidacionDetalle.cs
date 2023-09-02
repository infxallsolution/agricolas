using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Nomina.WebForms.App_Code.Liquidacion
{
    [Serializable]
    public class LiquidacionDetalle
    {
        public string RegistroDetalleNomina { get; set; }
        public string CodConcepto { get; set; }
        public string DescripcionConcepto { get; set; }
        public string Cantidad { get; set; }
        public string ValorUnitario { get; set; }
        public string ValorTotal { get; set; }
        public string Porcentaje { get; set; }
        public bool ValidaPorcentaje { get; set; }
        public bool BaseSeguridadSocial { get; set; }
        public bool Deduccion { get; set; }
        public bool HabilitaValorTotal { get; set; }
        public string saldo { get; set; }


        public LiquidacionDetalle()
        {

        }
    }
}