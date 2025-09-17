namespace AngularJsCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Desi_Id = c.Int(nullable: false, identity: true),
                        Desi_Name = c.String(),
                    })
                .PrimaryKey(t => t.Desi_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Emp_Id = c.Int(nullable: false, identity: true),
                        Emp_Name = c.String(),
                        Desi_Id = c.Int(),
                        Emp_Age = c.Int(nullable: false),
                        Join_Date = c.DateTime(nullable: false),
                        Gender = c.String(),
                        PicPath = c.String(),
                    })
                .PrimaryKey(t => t.Emp_Id)
                .ForeignKey("dbo.Designations", t => t.Desi_Id)
                .Index(t => t.Desi_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Desi_Id", "dbo.Designations");
            DropIndex("dbo.Employees", new[] { "Desi_Id" });
            DropTable("dbo.Employees");
            DropTable("dbo.Designations");
        }
    }
}
