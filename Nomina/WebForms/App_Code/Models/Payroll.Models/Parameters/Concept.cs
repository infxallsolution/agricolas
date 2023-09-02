using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters
{
    [Table("nConcepto")]
    public class Concept
    {
        [Key, Column(Order = 0)]
        public int empresa { get; set; }

        [Key, Column(Order = 1, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string codigo { get; set; }

        public string descripcion { get; set; }

        public string abreviatura { get; set; }
        public int signo { get; set; }
        public int tipoLiquidacion { get; set; }
        [Column(name: "base")]
        public string Base { get; set; }
        public decimal porcentaje { get; set; }
        public double valor { get; set; }
        public double valorMinimo { get; set; }
        public bool basePrimas { get; set; }
        public bool baseCajaCompensacion { get; set; }
        public bool baseCesantias { get; set; }
        public bool baseVacaciones { get; set; }
        public bool baseIntereses { get; set; }
        public bool baseSeguridadSocial { get; set; }
        public bool controlaSaldo { get; set; }
        public bool manejaRango { get; set; }
        public bool ingresoGravado { get; set; }
        public bool controlConcepto { get; set; }
        public bool activo { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public bool validaPorcentaje { get; set; }
        public bool fijo { get; set; }
        public bool baseEmbargo { get; set; }
        public bool prioridad { get; set; }
        public bool descuentaDomingo { get; set; }
        public bool descuentaTransporte { get; set; }
        public bool mostrarFecha { get; set; }
        public bool noMostrar { get; set; }
        public bool mostrarDetalle { get; set; }
        public bool ausentismo { get; set; }
        public string equivalencia { get; set; }
        public bool ausentismo1 { get; set; }
        public string equivalencia1 { get; set; }
        public bool manejaBase { get; set; }


    }
}