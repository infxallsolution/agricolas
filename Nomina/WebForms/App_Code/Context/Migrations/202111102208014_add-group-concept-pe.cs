namespace Nomina.WebForms.App_Code.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addgroupconceptpe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.nGrupoConceptoDetalleNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    ConceptId = c.String(maxLength: 50, unicode: false),
                    GroupConceptId = c.Guid(nullable: false),
                    Company = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.nConcepto", t => new { t.Company, t.ConceptId })
                .ForeignKey("dbo.nGrupoConceptoNE", t => t.GroupConceptId, cascadeDelete: true)
                .Index(t => new { t.Company, t.ConceptId })
                .Index(t => t.GroupConceptId);

            CreateTable(
                "dbo.nGrupoConceptoNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    TypeEquivalence = c.Int(nullable: false),
                    EquivalenceConcept = c.Int(nullable: false),
                    Company = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.nGrupoConceptoDetalleNE", "GroupConceptId", "dbo.nGrupoConceptoNE");
            DropForeignKey("dbo.nGrupoConceptoDetalleNE", new[] { "Company", "ConceptId" }, "dbo.nConcepto");
            DropIndex("dbo.nGrupoConceptoDetalleNE", new[] { "GroupConceptId" });
            DropIndex("dbo.nGrupoConceptoDetalleNE", new[] { "Company", "ConceptId" });
            DropTable("dbo.nGrupoConceptoNE");
            DropTable("dbo.nGrupoConceptoDetalleNE");
        }
    }
}