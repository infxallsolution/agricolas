using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    [Serializable]
    public class CtransaccionFertilizante
    {
        public CtransaccionFertilizante()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private string _idItem;

        public string IdItem
        {
            get { return _idItem; }
            set { _idItem = value; }
        }
        private string _item;

        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }
        private string _uMedidaItem;

        public string UMedidaItem
        {
            get { return _uMedidaItem; }
            set { _uMedidaItem = value; }
        }
        private decimal _cantidadItem;

        public decimal CantidadItem
        {
            get { return _cantidadItem; }
            set { _cantidadItem = value; }
        }
        private decimal _noBulto;

        public decimal NoBulto
        {
            get { return _noBulto; }
            set { _noBulto = value; }
        }
        private decimal _peBulto;

        public decimal PeBulto
        {
            get { return _peBulto; }
            set { _peBulto = value; }
        }
        private int _registroItem;

        public int RegistroItem
        {
            get { return _registroItem; }
            set { _registroItem = value; }
        }
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

        private string fechaD;

        public string FechaD
        {
            get { return fechaD; }
            set { fechaD = value; }
        }

        private decimal pesoRacimo;

        public decimal PesoRacimo
        {
            get { return pesoRacimo; }
            set { pesoRacimo = value; }
        }

        private decimal jornal;

        public decimal Jornal
        {
            get { return jornal; }
            set { jornal = value; }
        }


        private decimal precioLabor;

        public decimal PrecioLabor
        {
            get { return precioLabor; }
            set { precioLabor = value; }
        }

        private List<CtransaccionFertilizante> _listaItems;

        public List<CtransaccionFertilizante> ListaItems
        {
            get { return _listaItems; }
            set { _listaItems = value; }
        }

        private int _noPalmas;

        public int NoPalmas
        {
            get { return _noPalmas; }
            set { _noPalmas = value; }
        }

        private int _registroR;

        public int RegistroR
        {
            get { return _registroR; }
            set { _registroR = value; }
        }

        public CtransaccionFertilizante(string codnovedad, string desnovedad, string codlote, string deslote, string codseccion, string desseccion,
            decimal racimos, decimal cantidad, List<Ctercero> terceros, int registro, string umedida, decimal pesoRacimo,
            string fechaD, decimal jornal, decimal precioLabor, List<CtransaccionFertilizante> listaItem)
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
            this.fechaD = fechaD;
            this.pesoRacimo = pesoRacimo;
            this.jornal = jornal;
            this.precioLabor = precioLabor;
            this._listaItems = listaItem;
        }

        public CtransaccionFertilizante(string iditem, string item, string umedidaItem, decimal cantidadItem, decimal noBultos, decimal pesoBulto, int noPalmas, int registro, int registroR)
        {
            _idItem = iditem;
            _item = item;
            _uMedidaItem = umedidaItem;
            _cantidadItem = cantidadItem;
            _noBulto = noBultos;
            _peBulto = pesoBulto;
            _registroItem = registro;
            _noPalmas = noPalmas;
            _registroR = registroR;


        }


        public string retornaUmedidaCatalogo(string item, int empresa)
        {
            string[] iParametros = new string[] { "@producto", "@empresa" };
            string[] oParametros = new string[] { "@uMedida" };
            object[] objValores = new object[] { item, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaUmedidaCatalogoConsumo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaDsReferenciaTransaccion(string tipoTra, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTra, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaDsReferenciaTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView ejecutaReferencia(int empresa, string ds)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                ds,
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int verificaUmedidaFertilizacion(string umedida, int empresa)
        {
            string[] iParametros = new string[] { "@umedida", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { umedida, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spverificaUmedidaFertilizacion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int verificaSaldoItem(string numero, string item, string lote, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@item", "@lote", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero, item, lote, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spVerificaSaldoItem",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int actualizaSaldoTransaccion(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spActualizaSaldoItemTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int verificaUmedidaBulto(string umedida, int empresa)
        {
            string[] iParametros = new string[] { "@umedida", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { umedida, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "SpVerificaUmedidaBulto",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

    }
}