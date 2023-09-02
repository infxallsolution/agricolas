using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Output
{
    public class CiudadOutput
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public PaisOutput Pais { get; set; }
        public DepartamentoOutput Departamento { get; set; }
    }
}