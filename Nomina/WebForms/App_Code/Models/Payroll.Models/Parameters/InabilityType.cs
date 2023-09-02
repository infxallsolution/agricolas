using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters
{
    [Table("nTipoIncapacidad")]
    public class InabilityType
    {
        [Key, Column(Order = 0)]
        public int empresa { get; set; }

        [Key, Column(Order = 1, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string codigo { get; set; }

        public string descripcion { get; set; }

        public double porcentaje { get; set; }
        public bool adicionarPorcentaje { get; set; }
        public int despues { get; set; }
        public double porcentajeNuevo { get; set; }
        public bool activo { get; set; }
        public bool afectaSeguridadSocial { get; set; }
        public bool afectaARL { get; set; }
        public string afectaNovedadSS { get; set; }
        public bool calculaIBC { get; set; }

    }
}