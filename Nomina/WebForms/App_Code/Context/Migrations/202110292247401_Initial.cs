namespace Nomina.WebForms.App_Code.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.gCiudadNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    CityId = c.String(maxLength: 50, unicode: false),
                    Company = c.Int(nullable: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.gCiudad", t => new { t.Company, t.CityId })
                .Index(t => new { t.Company, t.CityId });

            CreateTable(
                "dbo.gPaisNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    CountryId = c.String(maxLength: 50, unicode: false),
                    Company = c.Int(nullable: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.gPais", t => new { t.Company, t.CountryId })
                .Index(t => new { t.Company, t.CountryId });


            CreateTable(
                "dbo.gDepartamentoNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    DepartmentId = c.String(maxLength: 50, unicode: false),
                    CountryId = c.String(maxLength: 50, unicode: false),
                    Company = c.Int(nullable: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.gDepartamento", t => new { t.Company, t.CountryId, t.DepartmentId })
                .Index(t => new { t.Company, t.CountryId, t.DepartmentId });



            CreateTable(
                "dbo.nSubTipoCotizanteNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    IdSubTypeContributor = c.String(maxLength: 50, unicode: false),
                    Company = c.Int(nullable: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.nSubTipoCotizante", t => new { t.Company, t.IdSubTypeContributor })
                .Index(t => new { t.Company, t.IdSubTypeContributor });

            CreateTable(
                "dbo.gValoresPorEquivalenciaNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Type = c.Int(nullable: false),
                    Entity = c.Int(nullable: false),
                    Key = c.String(nullable: false),
                    ExtraKey = c.String(),
                    Value = c.String(nullable: false),
                    Parent = c.String(),
                    EntityParent = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.nSubTipoCotizanteNE", new[] { "Company", "IdSubTypeContributor" }, "dbo.nSubTipoCotizante");
            DropForeignKey("dbo.gDepartamentoNE", new[] { "Company", "CountryId", "DepartmentId" }, "dbo.gDepartamento");
            DropForeignKey("dbo.gPaisNE", new[] { "Company", "CountryId" }, "dbo.gPais");
            DropForeignKey("dbo.gCiudadNE", new[] { "Company", "CityId" }, "dbo.gCiudad");
            DropIndex("dbo.nSubTipoCotizanteNE", new[] { "Company", "IdSubTypeContributor" });
            DropIndex("dbo.gDepartamentoNE", new[] { "Company", "CountryId", "DepartmentId" });
            DropIndex("dbo.gPaisNE", new[] { "Company", "CountryId" });
            DropIndex("dbo.gCiudadNE", new[] { "Company", "CityId" });
            DropTable("dbo.gValoresPorEquivalenciaNE");
            DropTable("dbo.nSubTipoCotizanteNE");
            DropTable("dbo.nSubTipoCotizante");
            DropTable("dbo.gDepartamentoNE");
            DropTable("dbo.gDepartamento");
            DropTable("dbo.gPaisNE");
            DropTable("dbo.gPais");
            DropTable("dbo.gCiudadNE");
            DropTable("dbo.gCiudad");
        }
    }
}