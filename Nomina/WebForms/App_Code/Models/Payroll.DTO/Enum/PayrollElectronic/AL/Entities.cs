using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL
{
    public enum Entities
    {
        [Display(Name = "Ciudad")]
        City,
        [Display(Name = "País")]
        Country,
        [Display(Name = "Departamento")]
        Deparment,
        [Display(Name = "Tipo de trabajador")]
        SubTypeContributibe,
        [Display(Name = "Tipo de documento")]
        DocumentType,
        [Display(Name = "Tipo de nómina")]
        PayrollType,
        [Display(Name = "Forma de pago")]
        PayType,
        [Display(Name = "Conceptos")]
        Concepts,
        [Display(Name = "Incapcacidad")]
        Inability,
        [Display(Name = "Licencias")]
        Licences,
        [Display(Name = "Clase de contrato")]
        ContractType
    }
}