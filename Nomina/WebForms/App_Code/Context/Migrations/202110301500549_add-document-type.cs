namespace Nomina.WebForms.App_Code.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class adddocumenttype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.gTipoDocumentoNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    DocumentId = c.String(maxLength: 50, unicode: false),
                    Company = c.Int(nullable: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.gTipoDocumento", t => new { t.Company, t.DocumentId })
                .Index(t => new { t.Company, t.DocumentId });
        }

        public override void Down()
        {
            DropForeignKey("dbo.gTipoDocumentoNE", new[] { "Company", "DocumentId" }, "dbo.gTipoDocumento");
            DropIndex("dbo.gTipoDocumentoNE", new[] { "Company", "DocumentId" });
            DropTable("dbo.gTipoDocumentoNE");
            DropTable("dbo.gTipoDocumento");
        }
    }
}