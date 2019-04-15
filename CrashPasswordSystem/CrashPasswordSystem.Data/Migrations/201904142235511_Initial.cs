namespace CrashPasswordSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrashCompany",
                c => new
                    {
                        CCID = c.Int(nullable: false, identity: true),
                        CCName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CCID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        PCID = c.Int(nullable: false),
                        CCID = c.Int(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                        ProductDescription = c.String(nullable: false, maxLength: 100),
                        ProductURL = c.String(nullable: false, maxLength: 200),
                        ProductUsername = c.String(nullable: false, maxLength: 40),
                        ProductPassword = c.String(nullable: false, maxLength: 100),
                        ProductDateAdded = c.DateTime(nullable: false),
                        ProductExpiry = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.ProductCategory", t => t.PCID)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID)
                .ForeignKey("dbo.Users", t => t.StaffID)
                .ForeignKey("dbo.CrashCompany", t => t.CCID)
                .Index(t => t.PCID)
                .Index(t => t.CCID)
                .Index(t => t.SupplierID)
                .Index(t => t.StaffID);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        PCID = c.Int(nullable: false, identity: true),
                        PCName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.PCID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 100),
                        SupplierAddress = c.String(maxLength: 200),
                        SupplierContactNumber = c.String(maxLength: 30),
                        SupplierEmail = c.String(maxLength: 100),
                        SupplierWebsite = c.String(maxLength: 100),
                        SupplierDateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.UpdateHistory",
                c => new
                    {
                        UHID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UHID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserFirstName = c.String(maxLength: 50),
                        UserLastName = c.String(maxLength: 50),
                        UserInitials = c.String(maxLength: 10, fixedLength: true),
                        UserEmail = c.String(maxLength: 100),
                        UserHash = c.String(maxLength: 100),
                        UserSalt = c.String(maxLength: 20),
                        UserDateCreated = c.DateTime(),
                        UserActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CCID", "dbo.CrashCompany");
            DropForeignKey("dbo.Products", "StaffID", "dbo.Users");
            DropForeignKey("dbo.UpdateHistory", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.Products", "PCID", "dbo.ProductCategory");
            DropIndex("dbo.UpdateHistory", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "StaffID" });
            DropIndex("dbo.Products", new[] { "SupplierID" });
            DropIndex("dbo.Products", new[] { "CCID" });
            DropIndex("dbo.Products", new[] { "PCID" });
            DropTable("dbo.Users");
            DropTable("dbo.UpdateHistory");
            DropTable("dbo.Suppliers");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Products");
            DropTable("dbo.CrashCompany");
        }
    }
}
