using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Cfuncionarios
    {
        public Cfuncionarios()
        {
            //
            // TODO: Add constructor logic here
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nFuncionario",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (id like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public string[] RetornaNombreTercero(string codigo, int empresa)
        {
            string[] nombreTercero = new string[5];
            object[] objValores = new object[] { codigo, empresa };

            foreach (DataRowView registro in CentidadMetodos.EntidadGetKey(
                "cTercero",
                "ppa",
                objValores).Tables[0].DefaultView)
            {
                nombreTercero.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(7)), 0);
                nombreTercero.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(5)), 1);
                nombreTercero.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(12)), 2);
                nombreTercero.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(0)), 3);
                nombreTercero.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(6)), 4);
            }

            return nombreTercero;
        }


        public string RetornaDescripcion(string codigo)
        {
            string[] iParametros = new string[] { "@funcionario" };
            string[] oParametros = new string[] { "@descripcion" };
            object[] objValores = new object[] { codigo };

            return Convert.ToString(Cacceso.ExecProc(
                "SpVerificaFuncionario",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }




        public int GuardaRegistroManual(string cuadrilla,
                    DateTime fecha,
                    DateTime fechaE,
                    DateTime fechaS,
                    string funcionario,
                    string tipo,
                    string turno,
                    string usuario)
        {
            string[] iParametros = new string[] {
           "@cuadrilla","@fecha","@fechaEntrada","@fechaSalida",
            "@funcionario","@tipoEntrada","@turno","@usuario"};

            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { cuadrilla, fecha, fechaE, fechaS, funcionario, tipo, turno, usuario };

            return Convert.ToInt32(Cacceso.ExecProc(
                "SpInsertanRegistroPorteriaManual",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int ActualizaRegistroManual(DateTime fecha, DateTime fechas, DateTime fechae, string funcionario)
        {
            string[] iParametros = new string[] { "@fecha", "@fechaEntrada", "@fechaSalida", "@funcionario" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { fecha, fechae, fechas, funcionario };

            return Convert.ToInt32(Cacceso.ExecProc(
                "SpActualizanRegistroPorteriaManual",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }




    }
}