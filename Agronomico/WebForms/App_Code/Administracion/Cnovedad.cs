using Agronomico.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Cnovedad
    {
        public Cnovedad()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("aNovedad", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%' )";

            return dvEntidad;
        }



        public DataView BuscarEntidadTipo(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "aTipoNovedad",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and ( tipo like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView PeriodoAño(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaAños",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaNovedadPrecios(int empresa, int año)
        {
            string[] iParametros = new string[] { "@empresa", "@año" };
            object[] objValores = new object[] { empresa, año };

            return Cacceso.DataSetParametros(
                "spSeleccionaNovedadPrecios",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public string[] RetornaConfigNovedad(string codigo, int empresa)
        {

            string[] novedad = new string[20];
            object[] objValores = new object[] { codigo, empresa };

            foreach (DataRowView registro in CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGetKey(
                "aNovedad",
                "ppa",
                objValores), "descripcion", empresa))
            {
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(5)), 0); //umedida
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(6)), 1);//ciclos
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(7)), 2);//tarea
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(8)), 3);//naturaleza
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(9)), 4);///impuesto
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(10)), 5);//equivalencia
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(11)), 6);//concepto
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(12)), 7);//grupoIR
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(13)), 8);//ManejaLote
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(14)), 9);//ManejaSaldo
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(15)), 10);//ManejaCanal
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(16)), 11);//ManejaLinea
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(17)), 12);//ManejaPalma
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(18)), 13);
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(19)), 14);
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(20)), 15);
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(21)), 16);
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(22)), 17);
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(23)), 18);
                novedad.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(24)), 19);

            }

            return novedad;
        }


        public int VerificaNovedadTipo(string tipo, int novedad, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo", "@novedad" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipo, novedad };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spVerificaNovedadTipo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int verificarRegistroPrecio(int año, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { año, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spVerificaRegistroPrecios",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int EliminaNovedades(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipo };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spEliminaNovedadesTipo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public decimal SeleccionaPrecioNovedadLote(int empresa, int novedad, string finca, string lote, bool manejaLote)
        {
            string[] iParametros = new string[] { "@empresa", "@novedad", "@finca", "@lote", "@manejaLote" };
            string[] oParametros = new string[] { "@precio" };
            object[] objValores = new object[] { empresa, novedad, finca, lote, manejaLote };


            return Convert.ToDecimal(Cacceso.ExecProc(
              "spSeleccionaPrecioLoteFincaNovedad",
              iParametros,
              oParametros,
              objValores,
              "ppa").GetValue(0));
        }

        public string NovedadConfig(string novedad, int empresa)
        {
            string retorno = "";
            object[] objKey = new object[] { novedad, empresa };

            foreach (DataRowView registro in CentidadMetodos.EntidadGetKey(
                "aNovedad",
                "ppa",
                objKey).Tables[0].DefaultView)
            {
                for (int i = 2; i < registro.Row.ItemArray.Length; i++)
                {
                    retorno = retorno + registro.Row.ItemArray.GetValue(i).ToString() + "*";
                }
            }

            return retorno;
        }

        public string Consecutivo(string grupo, int empresa)
        {
            string[] iParametros = new string[] { "@grupo", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { grupo, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoNovedades",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public bool ManejaCanalNovedad(string novedad, int empresa)
        {

            string[] iParametros = new string[] { "@novedad", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { novedad, empresa };

            return Convert.ToBoolean(Cacceso.ExecProc(
                "spManejaCanalNovedad",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));

        }

        public decimal SeleccionaMetrajeTipoCanalNovedad(string novedad, string lote, int empresa)
        {

            string[] iParametros = new string[] { "@novedad", "@lote", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { novedad, lote, empresa };

            return Convert.ToDecimal(Cacceso.ExecProc(
                "SeleccionaMetrajeTipoCanalNovedad",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));

        }

        public DataView RetornaDatosLabores(string codigo, int empresa)
        {

            string[] iParametros = new string[] { "@codigo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { codigo, empresa };


            return Cacceso.DataSetParametros(
                "spRetornaDatosLabores",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;

        }

        public int validaManejaPalmas(string novedad, int empresa)
        {
            string[] iParametros = new string[] { "@novedad", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { novedad, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spvalidaManejaPalmas",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));

        }

        public decimal SeleccionaPalmasLote(string lote, int empresa)
        {

            string[] iParametros = new string[] { "@lote", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { lote, empresa };

            return Convert.ToDecimal(Cacceso.ExecProc(
                "spSeleccionaPalmasLote",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));

        }


        public int validaManejaCaracteristicas(string novedad, int empresa)
        {
            string[] iParametros = new string[] { "@novedad", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { novedad, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spvalidaManejaCaracteristicas",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));

        }






    }
}