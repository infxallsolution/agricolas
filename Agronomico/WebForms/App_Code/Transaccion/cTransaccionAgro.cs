using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class cTransaccionAgro
    {
        public int item { get; set; }
        public string jerarquia { get; set; }
        public decimal pesoPromedio { get; set; }
        public decimal racimos { get; set; }
        public decimal totalKilogramos { get; set; }
        public decimal sacos { get; set; }
        public int añoSiembra { get; set; }
        public List<cTerceros> lCosecheros { get; set; }
        public List<cTerceros> lTransportadores { get; set; }
        public List<cTerceros> lCargadores { get; set; }

        public int guardaTiquete(int empresa,
            string tipo, string numero, string extractora, string tiquete,
            decimal pesoNeto, decimal sacos, decimal racimos, string cedulaConductor, string nombreConductor,
            DateTime fechaTiquete, bool interno, string vehiculo, string remolque
            )
        {
            string[] iParametros = new string[] { "@empresa" ,
"@tipo",
"@numero",
"@extractora",
"@tiquete",
"@pesoNeto",
"@sacos",
"@racimos",
"@cedulaConductor",
"@nombreConductor",
"@fechaTiquete" ,
"@interno", "@vehiculo","@remolque" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] {empresa, tipo, numero, extractora,
        tiquete, pesoNeto, sacos, racimos, cedulaConductor, nombreConductor, fechaTiquete, interno,vehiculo, remolque  };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spInsertaTiqueteAgro",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public int guardaEncabezado()
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@tipoBorrado" };
            object[] objValores = new object[] { };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spRetornaModoBorradoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public int guardaDetalle()
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@tipoBorrado" };
            object[] objValores = new object[] { };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spRetornaModoBorradoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
    }

    public class cTerceros
    {
        public string actividad { get; set; }
        public decimal cantidadActividad { get; set; }
        public string codigoTercero { get; set; }
        public string nombreTercero { get; set; }
        public string uMedidaActividad { get; set; }
        public decimal racimos { get; set; }
        public string fecha { get; set; }
        public decimal jornalesTercero { get; set; }
        public decimal precioLabor { get; set; }
    }

    public class cTiquete
    {
        public int empresa { get; set; }
        public string tipo { get; set; }
        public string numero { get; set; }
        public string extractora { get; set; }
        public string tiquete { get; set; }
        public decimal pesoNeto { get; set; }
        public decimal sacos { get; set; }
        public decimal racimos { get; set; }
        public string cedulaConductor { get; set; }
        public string nombreConductor { get; set; }
        public string fechaTiquete { get; set; }
        public bool interno { get; set; }
        public string vehiculo { get; set; }
        public string remolque { get; set; }
    }

    public class Csubtotales
    {
        public string novedades { get; set; }
        public string nombreNovedades { get; set; }
        public decimal subCantidad { get; set; }
        public decimal subRacimo { get; set; }
        public decimal subJornal { get; set; }
        public Csubtotales() { }
        public Csubtotales(string novedades, string nombreNovedades, decimal subCantidad, decimal subRacimo, decimal subJornal) { this.novedades = novedades; this.nombreNovedades = nombreNovedades; this.subCantidad = subCantidad; this.subRacimo = subRacimo; this.subJornal = subJornal; }
    }
}