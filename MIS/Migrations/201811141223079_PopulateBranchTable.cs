namespace MIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBranchTable : DbMigration
    {
        public override void Up()
        {

            Sql("Insert into Branches (Id, Name, Status) values (1, 'Balwyn', 1)");
            Sql("Insert into Branches (Id, Name, Status) values (2, 'Malvern', 1)");
            Sql("Insert into Branches (Id, Name, Status) values (3, 'Hampton', 1)");
            Sql("Insert into Branches (Id, Name, Status) values (4, 'Mount Eliza', 1)");
            Sql("Insert into Branches (Id, Name, Status) values (5, 'Albert Park', 1)");
            Sql("Insert into Branches (Id, Name, Status) values (6, 'Tooronga', 1)");


        }
        
        public override void Down()
        {
        }
    }
}
