using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Presupuesto
{
    public class Ctransacciones
    {
        private string _cuenta;

        public string Cuenta
        {
            get { return _cuenta; }
            set { _cuenta = value; }
        }
        private string _tercero;

        public string Tercero
        {
            get { return _tercero; }
            set { _tercero = value; }
        }
        private string _sucursal;

        public string Sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; }
        }
        private string _ccosto;

        public string Ccosto
        {
            get { return _ccosto; }
            set { _ccosto = value; }
        }
        private decimal _valorBase;

        public decimal ValorBase
        {
            get { return _valorBase; }
            set { _valorBase = value; }
        }
        private string _referencia;

        public string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }
        private decimal _debito;

        public decimal Debito
        {
            get { return _debito; }
            set { _debito = value; }
        }
        private decimal _credito;

        public decimal Credito
        {
            get { return _credito; }
            set { _credito = value; }
        }
        private string _detalle;

        public string Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }
        private int _registro;

        public int Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }

        private bool _anulado;

        public bool Anulado
        {
            get { return _anulado; }
            set { _anulado = value; }
        }


        public Ctransacciones(string cuenta, string tercero, string sucursal, string ccosto, decimal Valorbase, string referencia,
         decimal debito, decimal credito, string detalle, int registro, bool anulado)
        {
            _cuenta = cuenta;
            _tercero = tercero;
            _sucursal = sucursal;
            _ccosto = ccosto;
            _valorBase = Valorbase;
            _referencia = referencia;
            _debito = debito;
            _credito = credito;
            _detalle = detalle;
            _anulado = anulado;
            _registro = registro;
        }

        public Ctransacciones()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView GetCXPSaldoTercero(string tercero, string sucursal, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@sucursal", "@empresa" };
            object[] objValores = new object[] { tercero, sucursal, empresa };

            return Cacceso.DataSetParametros(
            "spGetCXPSaldoTercero",
            iParametros,
            objValores,
            "ppa").Tables[0].DefaultView;
        }

        public int EliminaRegistroTransaccion(string tipo, string numero, int empresa, int registro)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa", "@registro" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa, registro };
            return Convert.ToInt16(Cacceso.ExecProc("spEliminaRegistroTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public int aprobarTransaccionContabilidad(string tipo, string numero, int empresa, string usuario)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa, usuario };
            return Convert.ToInt16(Cacceso.ExecProc("spAprobarTransaccionContabilidad", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView GetTransaccionContable(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spGetTransaccionContable", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public int AnulaEgresos(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("spAnulaEgresos", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int contabilizarEgresos(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spContabilizaComprobanteEgreso", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int ActualizaEncabezadoContable(string numero, string tipo, int empresa, string usuario, string observacion, DateTime fecha, int año, int mes, string referencia, bool aprobado)
        {
            string[] iParametros = new string[] { "@numero", "@tipo", "@empresa", "@usuario", "@observacion", "@fecha", "@periodo", "@año", "@mes", "@referencia", "@ejecutado" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero, tipo, empresa, usuario, observacion, fecha, año.ToString() + mes.ToString(), año, mes, referencia, aprobado };

            return Convert.ToInt16(Cacceso.ExecProc("spActualizaEncabezadoContable", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }



        public DataView GetTransaccionCompleta(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };

            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaContabilidad", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionCompletaContabilidadCompra(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };

            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaContabilidadCompra", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetTransaccionCompletaEgresos(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };

            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaEgresos", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaCamposContabilidadDoc(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaCamposContabilidadDoc", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaCxCItemTercero(int empresa, string sucursal, string tercero, string item, decimal valor)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa", "@item", "@sucursal", "@valor" };
            object[] objValores = new object[] { tercero, empresa, item, sucursal, valor };

            return Cacceso.DataSetParametros("spSeleccionaCxCfactura", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int AnulaFacturaContable(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spAnulaFacturaContable", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public DataView GetSaldoTotalProducto(string producto, int empresa)
        {
            string[] iParametros = new string[] { "@producto", "@empresa" };
            object[] objValores = new object[] { producto, empresa };

            return Cacceso.DataSetParametros("spSaldoProductoTotal", iParametros, objValores, "planta").Tables[0].DefaultView;
        }

        public int AnulaIngresos(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("spAnulaDocumentoIngreso", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView GetTransaccionFacturas(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaFacturaContable", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView GetFacturaContableImpuesto(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spGetFacturaContableImpuesto", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int VerificaEdicionBorrado(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("spVerificaEdicionBorradoTransaccionesLabores", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public DataView GetFacturaContableDetalle(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spGetFacturaCantableDetalle", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetFacturaContableEncabezado(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spGetFacturaCantableEncabezado", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int ActualizaConsecutivo(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spActualizaConsecutivoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView GetTransaccionCajaOtros(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaIngresos", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView EjecutaFormulaA(string jerarquia, string variable, string varObj, string modo, DateTime fecha, int empresa)
        {
            string[] iParametros = new string[] { "@producto", "@movimiento", "@objVar", "@modo", "@fecha", "@empresa" };
            object[] objValores = new object[] { jerarquia, variable, varObj, modo, fecha, empresa };

            return Cacceso.DataSetParametros(
                "spEjecutaFormulaProduccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetMovimientoResultadoProducto(int producto, int empresa, string modulo)
        {
            string[] iParametros = new string[] { "@producto", "@empresa", "@modulo" };
            object[] objValores = new object[] { producto, empresa, modulo };

            return Cacceso.DataSetParametros(
                "spSeleccionaMovimientosResultadoProducto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetMovimientoResultadoProductoMostrar(int producto, int empresa, string modulo)
        {
            string[] iParametros = new string[] { "@producto", "@empresa", "@modulo" };
            object[] objValores = new object[] { producto, empresa, modulo };

            return Cacceso.DataSetParametros("spSeleccionaMovimientosResultadoProductoMostrar",
                iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView GetProductos(string planta, int empresa)
        {
            string[] iParametros = new string[] { "@planta", "@empresa" };
            object[] objValores = new object[] { planta, empresa };

            return Cacceso.DataSetParametros(
                "SpSeleccionaProductosPlanta",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int AnulaTransaccion(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo", "@numero", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipo, numero, usuario };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spAnulaTrnPresupuesto",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public DataView GetMovimientoResultadoProductoMostrar(string producto, int empresa)
        {
            string[] iParametros = new string[] { "@producto", "@empresa" };
            object[] objValores = new object[] { producto, empresa };

            return Cacceso.DataSetParametros("spSeleccionaMovimientosResultadoProductoMostrar",
                iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetProductoTransaccion(string transaccion, int empresa)
        {
            string[] iParametros = new string[] { "@transaccion", "@empresa" };
            object[] objValores = new object[] { transaccion, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaProductoTransaccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView GetTransaccionCompletaLaboratorio(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaTransaccionCompletalTransaccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetCamposEntidades(string id1, string id2)
        {
            string[] iParametros = new string[] { "@id1", "@id2" };
            object[] objValores = new object[] { id1, id2 };

            return Cacceso.DataSetParametros(
                "spSeleccionaCamposEntidadesII",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView GetTipoTransaccionModulo(int empresa)
        {
            DataView dvTipoTransaccion = CentidadMetodos.EntidadGet(
                "gTipoTransaccion",
                "ppa").Tables[0].DefaultView;

            dvTipoTransaccion.RowFilter = "modulo= '" + ConfigurationManager.AppSettings["modulo"].ToString() + "'" + " and empresa = " + empresa.ToString();
            dvTipoTransaccion.Sort = "descripcion";

            return dvTipoTransaccion;
        }

        public string RetornaNumeroTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaConsecutivoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }




        public DataView GetMovimientoResultadoFormulacion(string formulacion, int empresa, string modulo, string tipotransaccion)
        {
            string[] iParametros = new string[] { "@formulacion", "@empresa", "@modulo", "@tipotransaccion" };
            object[] objValores = new object[] { formulacion, empresa, modulo, tipotransaccion };

            return Cacceso.DataSetParametros(
                "spGetMovimientoResultadoPresupuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView EjecutaFormulaPresupuesto(
            string formulacion, string variable, string varObj, string modo, int año,
            int mes, int empresa, string perioricidad, string tipotransaccion)
        {
            string[] iParametros = new string[] { "@formulacion", "@movimiento", "@objVar", "@modo", "@año", "@mes", "@empresa", "@periodicidad", "@tipotransaccion" };
            object[] objValores = new object[] { formulacion, variable, varObj, modo, año, mes, empresa, perioricidad, tipotransaccion };

            return Cacceso.DataSetParametros(
                "spEjecutaFormulaPresupuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView RetornaDatosPresupuestoDetalle(
          string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaDatosPresupuestoDetalle",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }



        public int verificaRegistroTransaccionPresupuesto(int empresa, string tipo, string formulacion, int año)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo", "@formulacion", "@año" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipo, formulacion, año };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaRegistroTransaccionPresupuesto",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

    }
}