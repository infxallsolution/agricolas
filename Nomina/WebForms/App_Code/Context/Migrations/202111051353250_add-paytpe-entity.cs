namespace Nomina.WebForms.App_Code.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addpaytpeentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.gFormaPagoNE",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Company = c.Int(nullable: false),
                    PayTypeId = c.String(maxLength: 50, unicode: false),
                    EquivalenceAL = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.gFormaPago", t => new { t.Company, t.PayTypeId })
                .Index(t => new { t.Company, t.PayTypeId });

        }

        public override void Down()
        {
            DropForeignKey("dbo.gFormaPagoNE", new[] { "Company", "PayTypeId" }, "dbo.gFormaPago");
            DropIndex("dbo.gFormaPagoNE", new[] { "Company", "PayTypeId" });
            DropTable("dbo.gFormaPagoNE");
            DropTable("dbo.gFormaPago");
        }
    }
}