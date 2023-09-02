using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class Clote
    {
        private string codigo;
        private int racimos;

        public int Racimos
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


        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private string desLote;

        public string DesLote
        {
            get { return desLote; }
            set { desLote = value; }
        }

        private List<Ctercero> listaTercero;

        public List<Ctercero> ListaTercero
        {
            get { return listaTercero; }
            set { listaTercero = value; }
        }


        public Clote()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public Clote(string codigo, string desLote, int racimos, decimal jornales, List<Ctercero> listaTercero
            )
        {
            this.codigo = codigo;
            this.desLote = desLote;
            this.listaTercero = listaTercero;
            this.jornales = jornales;
            this.racimos = racimos;

        }




    }
}