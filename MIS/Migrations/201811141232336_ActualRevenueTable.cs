namespace MIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualRevenueTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActualRevenues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        WeekEndDate = c.DateTime(nullable: false),
                        BranchId = c.Byte(nullable: false),
                        CraetedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActualRevenues", "BranchId", "dbo.Branches");
            DropIndex("dbo.ActualRevenues", new[] { "BranchId" });
            DropTable("dbo.ActualRevenues");
        }
    }
}
