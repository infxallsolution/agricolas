using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Liquidacion
{
    public class CseguridadSocial
    {
        public CseguridadSocial()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(int empresa, int año, int mes, string texto)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("nSeguridadSocialPila", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and año =" + Convert.ToString(año) + " and mes =" + Convert.ToString(mes) +
                " and ( nombre1  like '%" + texto + "%' or nombre2  like '%" + texto + "%' or apellido1  like '%" + texto + "%' or apellido2  like '%" + texto + "%' or codigoTercero  like '%" + texto + "%' )";
            return dvEntidad;
        }

        public DataView LiquidaSeguridadSocial(int empresa, int año, int mes)
        {
            string[] iParametros = new string[] { "@año", "@mes", "@empresa" };
            object[] objValores = new object[] { año, mes, empresa };

            return Cacceso.DataSetParametros("spLiquidarSeguridadSocialPeriodo2388", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView LiquidaSeguridadSocialRegistro(int empresa, int año, int mes, int registro)
        {
            string[] iParametros = new string[] { "@año", "@mes", "@empresa", "@registro" };
            object[] objValores = new object[] { año, mes, empresa, registro };

            return Cacceso.DataSetParametros("spLiquidarSeguridadSocialPeriodoSelector2388", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

    }
}