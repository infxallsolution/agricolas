using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Output
{
    public class PaisOutput
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public List<DepartamentoOutput> Departamentos { get; set; }
    }
}