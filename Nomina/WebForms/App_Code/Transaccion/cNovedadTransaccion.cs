using Nomina.WebForms.App_Code.Administracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Transaccion
{
    public class cNovedadTransaccion
    {

        private string codnovedad;

        public string Codnovedad
        {
            get { return codnovedad; }
            set { codnovedad = value; }
        }
        private string desnovedad;

        public string Desnovedad
        {
            get { return desnovedad; }
            set { desnovedad = value; }
        }
        private string codlote;

        public string Codlote
        {
            get { return codlote; }
            set { codlote = value; }
        }
        private string deslote;

        public string Deslote
        {
            get { return deslote; }
            set { deslote = value; }
        }

        private string codseccion;

        public string Codseccion
        {
            get { return codseccion; }
            set { codseccion = value; }
        }

        private string desseccion;

        public string Desseccion
        {
            get { return desseccion; }
            set { desseccion = value; }
        }

        private decimal racimos;

        public decimal Racimos
        {
            get { return racimos; }
            set { racimos = value; }
        }
        private decimal jornales;

        public decimal Jornales
        {
            get { return jornales; }
            set { jornales = value; }
        }

        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private int registro;

        public int Registro
        {
            get { return registro; }
            set { registro = value; }
        }
        private List<Ctercero> terceros;

        public List<Ctercero> Terceros
        {
            get { return terceros; }
            set { terceros = value; }
        }

        private string umedida;

        public string Umedida
        {
            get { return umedida; }
            set { umedida = value; }
        }


        public cNovedadTransaccion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public cNovedadTransaccion(string codnovedad, string desnovedad, string codlote, string deslote, string codseccion, string desseccion, decimal racimos, decimal cantidad, List<Ctercero> terceros, int registro, string umedida)
        {
            this.codnovedad = codnovedad;
            this.desnovedad = desnovedad;
            this.codlote = codlote;
            this.deslote = deslote;
            this.racimos = racimos;
            this.terceros = terceros;
            this.registro = registro;
            this.codseccion = codseccion;
            this.desseccion = desseccion;
            this.umedida = umedida;
            this.cantidad = cantidad;
        }


        public string VerificaCargaExcelNovedades(int empresa, string concepto, string empleado, string añoI, string periodoI, string añoF, string periodoF)
        {
            string[] iParametros = new string[] { "@empresa", "@concepto", "@empleado", "@añoI", "@periodoInicial", "@añoF", "@periodoFinal" };
            object[] objValores = new object[] { empresa, concepto, empleado, añoI, periodoI, añoF, periodoF };
            string[] oParametros = new string[] { "@retorno" };
            return Convert.ToString(Cacceso.ExecProc("spVerificaCargaExcelNovedades", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int insertaTmpDetalle(int empresa, string tipo, string numero, int registro, string empleado
           , string concepto, decimal cantidad, decimal valor, string añoI, string periodoI, string añoF, string periodoF, int frecuencia, string detalle)
        {
            string[] iParametros = new string[] { "@empresa","@tipo", "@numero", "@registro", "@empleado", "@concepto","@cantidad" , "@valor",
            "@añoI", "@periodoInicial", "@añoF", "@periodoFinal" , "@frecuencia", "@detalle"};
            object[] objValores = new object[] { empresa, tipo, numero, registro, empleado, concepto, cantidad, valor, añoI, periodoI, añoF, periodoF, frecuencia, detalle };
            string[] oParametros = new string[] { "@retorno" };
            return Convert.ToInt16(Cacceso.ExecProc("spInsertaTmpNovedadDetalle", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int EliminaTmpNovedadDetalle(int empresa, string tipo, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo", "@numero" };
            object[] objValores = new object[] { empresa, tipo, numero };
            string[] oParametros = new string[] { "@retorno" };

            return Convert.ToInt16(Cacceso.ExecProc("spEliminaTmpNovedadDetalle", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int InsertaNovedadDetalle(int empresa, string tipo, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo", "@numero" };
            object[] objValores = new object[] { empresa, tipo, numero };
            string[] oParametros = new string[] { "@retorno" };

            return Convert.ToInt16(Cacceso.ExecProc("spInsertaNovedadDetalle", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView retornaTmpDetalle(int empresa, string tipo, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo", "@numero" };
            object[] objValores = new object[] { empresa, tipo, numero };

            return Cacceso.DataSetParametros(
                "spSeleccionaTmpNovedadDetalle",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
    }
}