using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Parametros
{
    public class Citems
    {
        public Citems()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private string codigo;
        private string descripcion;
        private string descripcionCorta;
        private string uMedidaConsumo;
        private string uMedidaCompra;
        private string referencia;
        private string tipo;




        public string Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public string DescripcionCorta
        {
            get
            {
                return descripcionCorta;
            }

            set
            {
                descripcionCorta = value;
            }
        }
        public string Referencia
        {
            get
            {
                return referencia;
            }

            set
            {
                referencia = value;
            }
        }
        public string UMedidaConsumo
        {
            get
            {
                return uMedidaConsumo;
            }

            set
            {
                uMedidaConsumo = value;
            }
        }

        public string UMedidaCompra
        {
            get
            {
                return uMedidaCompra;
            }

            set
            {
                uMedidaCompra = value;
            }
        }



        public string Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet(
              "iItems",
              "ppa").Tables[0].DefaultView;

            int number1 = 0;

            try
            {
                dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  codigo= " + Convert.ToInt16(texto).ToString()
                     + " or equivalencia like '%" + texto + "%'"
                    + " or referencia like '%" + texto + "%'"
                    + ")";
            }
            catch
            {
                dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and tipo in ('I','S') and (descripcion like '%" + texto + "%' "
              + " or equivalencia like '%" + texto + "%'"
                    + " or referencia like '%" + texto + "%'"
                    + ")";
            }

            return dvEntidad;
        }

        public string Consecutivo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaConsecutivoItems",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaPapeleta(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@papeleta" };
            object[] objValores = new object[] { empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaPapepeletaCatalogo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView ConsultaMayorPlan(string plan, int empresa)
        {
            string[] iParametros = new string[] { "@plan", "@empresa" };
            object[] objValores = new object[] { plan, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaMayoresPlanItems",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView ConsultaCriteriosItems(int item, int empresa)
        {
            string[] iParametros = new string[] { "@item", "@empresa" };
            object[] objValores = new object[] { item, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaCriteriosItem",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public string RetornaUmedida(string producto, int empresa)
        {
            string[] iParametros = new string[] { "@producto", "@empresa" };
            string[] oParametros = new string[] { "@uMedida" };
            object[] objValores = new object[] { producto, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaUmedidaCatalogo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaUmedidaConsumo(string producto, int empresa)
        {
            string[] iParametros = new string[] { "@producto", "@empresa" };
            string[] oParametros = new string[] { "@uMedida" };
            object[] objValores = new object[] { producto, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaUmedidaCatalogoConsumo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaDescripcion(string producto, int empresa)
        {
            string[] iParametros = new string[] { "@producto", "@empresa" };
            string[] oParametros = new string[] { "@descripcion" };
            object[] objValores = new object[] { producto, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaDescripcionProducto",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public DataView RetornaGruposImpuesto(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaGrupoImpuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaDatosItem(int codigo, int empresa)
        {
            string[] iParametros = new string[] { "@codigo", "@empresa" };
            object[] objValores = new object[] { codigo, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaDatosItem",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView Items()
        {
            string[] iParametros = new string[] { };
            object[] objValores = new object[] { };

            return Cacceso.DataSetParametros(
                "spSeleccionaItems",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaSaldoItem(int item, int empresa, string bodega)
        {
            string[] iParametros = new string[] { "@item", "@empresa", "@bodega" };
            object[] objValores = new object[] { item, empresa, bodega };

            return Cacceso.DataSetParametros(
                "spRetornaSaldoItem",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaItemsSuministro(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaItemsSuministros",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaItemsFiltro(int empresa, string filtro)
        {
            string[] iParametros = new string[] { "@empresa", "@filtro" };
            object[] objValores = new object[] { empresa, filtro };

            return Cacceso.DataSetParametros(
                "spRetornaItemsFiltro",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


    }
}