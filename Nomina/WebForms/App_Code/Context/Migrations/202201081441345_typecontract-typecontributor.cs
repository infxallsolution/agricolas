namespace Nomina.WebForms.App_Code.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class typecontracttypecontributor : DbMigration
    {
        public override void Up()
        {


            CreateTable(
                "dbo.nClaseContratoNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    ContractTypeId = c.String(maxLength: 50, unicode: false),
                    Company = c.Int(nullable: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.nClaseContrato", t => new { t.Company, t.ContractTypeId })
                .Index(t => new { t.Company, t.ContractTypeId });


            CreateTable(
                "dbo.nTipoCotizanteNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    IdTypeContributor = c.String(maxLength: 50, unicode: false),
                    Company = c.Int(nullable: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.nTipoCotizante", t => new { t.Company, t.IdTypeContributor })
                .Index(t => new { t.Company, t.IdTypeContributor });

        }

        public override void Down()
        {
            DropForeignKey("dbo.nTipoCotizanteNE", new[] { "Company", "IdTypeContributor" }, "dbo.nTipoCotizante");
            DropForeignKey("dbo.nClaseContratoNE", new[] { "Company", "ContractTypeId" }, "dbo.nClaseContrato");
            DropIndex("dbo.nTipoCotizanteNE", new[] { "Company", "IdTypeContributor" });
            DropIndex("dbo.nClaseContratoNE", new[] { "Company", "ContractTypeId" });
            DropTable("dbo.nTipoCotizanteNE");
            DropTable("dbo.nTipoCotizante");
            DropTable("dbo.nClaseContratoNE");
            DropTable("dbo.nClaseContrato");
        }
    }
}