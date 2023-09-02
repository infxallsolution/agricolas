using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class CnovedadCosecha
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
        private List<CterceroTiquete> terceros;

        public List<CterceroTiquete> Terceros
        {
            get { return terceros; }
            set { terceros = value; }
        }
        private List<CterceroTiquete> tercerosCargue;

        public List<CterceroTiquete> TercerosCargue
        {
            get { return tercerosCargue; }
            set { tercerosCargue = value; }
        }

        private List<CterceroTiquete> terceroTransporte;

        public List<CterceroTiquete> TerceroTransporte
        {
            get { return terceroTransporte; }
            set { terceroTransporte = value; }
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


        private int registroNovedad;

        public int RegistroNovedad
        {
            get { return registroNovedad; }
            set { registroNovedad = value; }
        }

        private string novedadCargue;

        public string NovedadCargue
        {
            get { return novedadCargue; }
            set { novedadCargue = value; }
        }
        private string nombrenovedadCargue;

        public string NombreNovedadCargue
        {
            get { return nombrenovedadCargue; }
            set { nombrenovedadCargue = value; }
        }


        private string fechaDcargue;

        public string FechaDcargue
        {
            get { return fechaDcargue; }
            set { fechaDcargue = value; }
        }
        private decimal precioCargue;

        public decimal PrecioCargue
        {
            get { return precioCargue; }
            set { precioCargue = value; }
        }
        private string novedadTransporte;

        public string NovedadTransporte
        {
            get { return novedadTransporte; }
            set { novedadTransporte = value; }
        }

        private string nombrenovedadTransporte;

        public string NombreNovedadTransporte
        {
            get { return nombrenovedadTransporte; }
            set { nombrenovedadTransporte = value; }
        }


        private string fechaDTransporte;

        public string FechaDTransporte
        {
            get { return fechaDTransporte; }
            set { fechaDTransporte = value; }
        }
        private decimal precioTransporte;

        public decimal PrecioTransporte
        {
            get { return precioTransporte; }
            set { precioTransporte = value; }
        }


        public CnovedadCosecha()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public CnovedadCosecha(string codnovedad, string desnovedad, string codlote, string deslote, string codseccion, string desseccion,
            decimal racimos, decimal cantidad, List<CterceroTiquete> terceros, int registro, string umedida, decimal pesoRacimo, string fechaD, decimal jornal, decimal precioLabor)
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

        }


    }
}