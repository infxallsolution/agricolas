using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Liquidacion
{
    public class CpagosNomina
    {
        public CpagosNomina()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }





        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            int i = 0;
            bool resultado = int.TryParse(texto, out i);

            if (resultado == true)
            {
                dvEntidad = CentidadMetodos.EntidadGet(
                    "nPagosNomina",
                    "ppa").Tables[0].DefaultView;
                dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (año = '" + texto + "' or periodoNomina = '" + texto + ")";

            }
            else
            {

                dvEntidad = CentidadMetodos.EntidadGet(
                   "nPagosNomina",
                   "ppa").Tables[0].DefaultView;
                dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa);

            }




            return dvEntidad;
        }


        public DataView RetornaDatosSeguridadSocial(int empresa, int año, int mes)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@mes" };
            object[] objValores = new object[] { empresa, año, mes };

            return Cacceso.DataSetParametros("spGeneraPlanoSeguridadSocial2388", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }



        public DataView RetornaDatosPagosNomina(int empresa, int periodo, int año, int mes, string documento)
        {
            string[] iParametros = new string[] { "@empresa", "@periodo", "@año", "@mes", "@documento" };
            object[] objValores = new object[] { empresa, periodo, año, mes, documento };

            return Cacceso.DataSetParametros("spRetornaDatosPagosNomina", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView GeneraPlanoPagoNominaDefenitiva(int empresa, string numero, int año, int periodo)
        {
            string[] iParametros = new string[] { "@empresa", "@numero", "@año", "@periodo" };
            object[] objValores = new object[] { empresa, numero, año, periodo };
            return Cacceso.DataSetParametros("spGeneraPlanoPagoNominaDefenitiva", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public int VerificaPeridoPagadoNomina(int empresa, int periodo, int año, string documentoNomina)
        {
            string[] iParametros = new string[] { "@empresa", "@periodo", "@año", "@documentoNomina" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, periodo, año, documentoNomina };

            return Convert.ToInt32(Cacceso.ExecProc(
                "VerificaPeridoPagadoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int AnulaPeridoPagadoNomina(int empresa, int periodo, int año, string usuario, string documento)
        {
            string[] iParametros = new string[] { "@empresa", "@periodo", "@año", "@usuario", "@documento" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, periodo, año, usuario, documento };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spAnulaPeridoPagadoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public DataView RetornaDatosDePagosxFormaPago(int empresa, string numero, int año, int periodo)
        {
            string[] iParametros = new string[] { "@empresa", "@numero", "@año", "@periodo" };
            object[] objValores = new object[] { empresa, numero, año, periodo };
            return Cacceso.DataSetParametros("spRetornaDatosDePagosxPeriodo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public string RetornaNombreArchivoPlano(int empresa, int año, int periodo)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@periodo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, año, periodo };

            return Convert.ToString(Cacceso.ExecProc(
                    "spRetornaNombreArchivoPlano",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }




    }
}