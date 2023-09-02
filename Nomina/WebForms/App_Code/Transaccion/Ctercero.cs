using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Transaccion
{
    public class Ctercero
    {

        private int codtercero;

        public int Codtercero
        {
            get { return codtercero; }
            set { codtercero = value; }
        }


        private string desTercero;

        public string DesTercero
        {
            get { return desTercero; }
            set { desTercero = value; }
        }
        private string lote;

        public string Lote
        {
            get { return lote; }
            set { lote = value; }
        }
        private string cuadrilla;

        public string Cuadrilla
        {
            get { return cuadrilla; }
            set { cuadrilla = value; }
        }
        private decimal precio;

        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private decimal valTotal;

        public decimal ValTotal
        {
            get { return valTotal; }
            set { valTotal = value; }
        }



        public Ctercero()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public Ctercero(int codTercero, string desTercero, string lote, string cuadrilla, decimal precio, decimal cantidad)
        {

            this.codtercero = codTercero;
            this.desTercero = desTercero;
            this.cuadrilla = cuadrilla;
            this.lote = lote;
            this.precio = precio;
            this.cantidad = cantidad;
            this.valTotal = cantidad * precio;

        }


    }
}