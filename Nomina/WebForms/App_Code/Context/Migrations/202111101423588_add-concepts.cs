namespace Nomina.WebForms.App_Code.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addconcepts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.nConceptoNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    ConceptId = c.String(maxLength: 50, unicode: false),
                    Company = c.Int(nullable: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.nConcepto", t => new { t.Company, t.ConceptId })
                .Index(t => new { t.Company, t.ConceptId });

        }

        public override void Down()
        {
            DropForeignKey("dbo.nConceptoNE", new[] { "Company", "ConceptId" }, "dbo.nConcepto");
            DropIndex("dbo.nConceptoNE", new[] { "Company", "ConceptId" });
            DropTable("dbo.nConceptoNE");
            DropTable("dbo.nConcepto");
        }
    }
}