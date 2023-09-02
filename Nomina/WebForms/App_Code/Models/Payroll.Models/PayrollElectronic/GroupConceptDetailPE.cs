using Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Nomina.WebForms.Repository.RepositoryExtension;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.PayrollElectronic
{
    [Table("nGrupoConceptoDetalleNE")]
    public class GroupConceptDetailPE : Entity<Guid>
    {
        [StringLength(50)]
        public string ConceptId { get; set; }
        public Guid GroupConceptId { get; set; }
        public int Company { get; set; }

        [ForeignKey("Company, ConceptId")]
        public Concept Concept { get; set; }
        [ForeignKey("GroupConceptId")]
        public GroupConceptPE GroupConceptsPE { get; set; }
    }
}