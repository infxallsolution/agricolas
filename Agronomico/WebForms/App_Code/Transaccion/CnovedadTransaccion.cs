using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class cNovedadTransaccion
    {

        private string codFinca;

        public string CodFinca
        {
            get { return codFinca; }
            set { codFinca = value; }
        }
        private string desFinca;

        public string DesFinca
        {
            get { return desFinca; }
            set { desFinca = value; }
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

        private decimal sacos;

        public decimal Sacos
        {
            get { return sacos; }
            set { sacos = value; }
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




        public cNovedadTransaccion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public cNovedadTransaccion(string codFinca, string desFinca, string codnovedad, string desnovedad, string codlote, string deslote, string codseccion, string desseccion,
            decimal racimos, decimal cantidad, List<Ctercero> terceros, int registro, string umedida, decimal pesoRacimo,
            string fechaD, decimal jornal, decimal precioLabor, decimal sacos)
        {
            this.codFinca = codFinca;
            this.desFinca = desFinca;
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
            this.sacos = sacos;
        }
    }
}