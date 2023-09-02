using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Parametros
{
    public class ClItems
    {
        public ClItems()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public ClItems(string codigo, string descripcion, string umedida, string bodega, decimal cantidad, decimal valUnitario, int registro, string desBodega)
        {
            this.Codigo = codigo;
            this.Descripcion = descripcion;
            this.Umedida = umedida;
            this.Bodega = bodega;
            this.Cantidad = cantidad;
            this.ValUnitario = valUnitario;
            this.Registro = registro;
            this.DesBodega = desBodega;
        }

        private string codigo;
        private string descripcion;
        private string umedida;
        private string bodega;
        private decimal cantidad;
        private decimal valUnitario;
        private int registro;
        private string desBodega;

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

        public string Umedida
        {
            get
            {
                return umedida;
            }

            set
            {
                umedida = value;
            }
        }

        public string Bodega
        {
            get
            {
                return bodega;
            }

            set
            {
                bodega = value;
            }
        }

        public decimal Cantidad
        {
            get
            {
                return cantidad;
            }

            set
            {
                cantidad = value;
            }
        }

        public decimal ValUnitario
        {
            get
            {
                return valUnitario;
            }

            set
            {
                valUnitario = value;
            }
        }

        public int Registro
        {
            get
            {
                return registro;
            }

            set
            {
                registro = value;
            }
        }

        public string DesBodega
        {
            get
            {
                return desBodega;
            }

            set
            {
                desBodega = value;
            }
        }
    }
}