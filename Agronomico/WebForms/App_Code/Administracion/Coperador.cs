using Agronomico.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Coperador
    {

        #region Atributos

        private string _campo;
        private string _operador;
        private string _valor;
        private string _valor2;

        public string Valor2
        {
            get { return _valor2; }
            set { _valor2 = value; }
        }

        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public string Operador
        {
            get { return _operador; }
            set { _operador = value; }
        }

        public string Campo
        {
            get { return _campo; }
            set { _campo = value; }
        }

        #endregion Atributos


        public Coperador()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public Coperador(string campo, string operador, string valor, string valor2)
        {
            _campo = campo;
            _operador = operador;
            _valor = valor;
            _valor2 = valor2;
        }

        public string FormatoWhere(List<Coperadores> operadores)
        {
            string where = "";
            int i = 0;

            foreach (object registro in operadores.ToArray())
            {
                if (i == 0)
                {
                    switch (Convert.ToString(operadores[i].Operador))
                    {
                        case "like":

                            where = where + Convert.ToString(operadores[i].Campo) + " like '%" + Convert.ToString(operadores[i].Valor) + "%' ";
                            break;

                        case "between":

                            where = where + Convert.ToString(operadores[i].Campo) + " between '" + Convert.ToString(operadores[i].Valor) + "' and '" +
                                Convert.ToString(operadores[i].Valor2) + " '";
                            break;

                        default:

                            where = where + Convert.ToString(operadores[i].Campo) + " " + Convert.ToString(operadores[i].Operador) + " '" +
                                Convert.ToString(operadores[i].Valor) + "' ";
                            break;
                    }
                }
                else
                {
                    switch (Convert.ToString(operadores[i].Operador))
                    {
                        case "like":

                            where = where + " and " + Convert.ToString(operadores[i].Campo) + " like '%" + Convert.ToString(operadores[i].Valor) + "%' ";
                            break;

                        case "between":

                            where = where + " and " + Convert.ToString(operadores[i].Campo) + " between '" + Convert.ToString(operadores[i].Valor) + "' and '" +
                                Convert.ToString(operadores[i].Valor2) + " '";
                            break;

                        default:

                            where = where + " and " + Convert.ToString(operadores[i].Campo) + " " + Convert.ToString(operadores[i].Operador) + " '" +
                                Convert.ToString(operadores[i].Valor) + "' ";
                            break;
                    }
                }

                i++;
            }

            return where;
        }

    }
}