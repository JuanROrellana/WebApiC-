namespace WEbApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                        ContactName = c.String(),
                        TelephoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        Amount = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Customers_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.Customers_CustomerID)
                .Index(t => t.Customers_CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customers_CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customers_CustomerID" });
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
