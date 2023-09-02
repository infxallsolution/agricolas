namespace Nomina.WebForms.App_Code.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addinabilitylicence : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.nTipoIncapacidadNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Company = c.Int(nullable: false),
                    InabilityTypeId = c.String(maxLength: 50, unicode: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.nTipoIncapacidad", t => new { t.Company, t.InabilityTypeId })
                .Index(t => new { t.Company, t.InabilityTypeId });

            CreateTable(
                "dbo.nTipoIncapacidadLicenciaNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Company = c.Int(nullable: false),
                    LicenceTypeId = c.String(maxLength: 50, unicode: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.nTipoIncapacidad", t => new { t.Company, t.LicenceTypeId })
                .Index(t => new { t.Company, t.LicenceTypeId });

        }

        public override void Down()
        {
            DropForeignKey("dbo.nTipoIncapacidadLicenciaNE", new[] { "Company", "LicenceTypeId" }, "dbo.nTipoIncapacidad");
            DropForeignKey("dbo.nTipoIncapacidadNE", new[] { "Company", "InabilityTypeId" }, "dbo.nTipoIncapacidad");
            DropIndex("dbo.nTipoIncapacidadLicenciaNE", new[] { "Company", "LicenceTypeId" });
            DropIndex("dbo.nTipoIncapacidadNE", new[] { "Company", "InabilityTypeId" });
            DropTable("dbo.nTipoIncapacidadLicenciaNE");
            DropTable("dbo.nTipoIncapacidadNE");
            DropTable("dbo.nTipoIncapacidad");
        }
    }
}