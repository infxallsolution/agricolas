namespace Nomina.WebForms.App_Code.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addpayrollentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.nTipoNominaNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Company = c.Int(nullable: false),
                    PayrollTypeId = c.String(maxLength: 50, unicode: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.nTipoNomina", t => new { t.Company, t.PayrollTypeId })
                .Index(t => new { t.Company, t.PayrollTypeId });

        }

        public override void Down()
        {
            DropForeignKey("dbo.nTipoNominaNE", new[] { "Company", "PayrollTypeId" }, "dbo.nTipoNomina");
            DropIndex("dbo.nTipoNominaNE", new[] { "Company", "PayrollTypeId" });
            DropTable("dbo.nTipoNominaNE");
            DropTable("dbo.nTipoNomina");
        }
    }
}