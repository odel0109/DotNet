namespace StoreApp.ProductData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        DefaultName = c.String(nullable: false, maxLength: 64),
                        NameMessageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        DefaultName = c.String(nullable: false, maxLength: 64),
                        NameMessageID = c.Int(nullable: false),
                        DescriptionMessageID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 10, scale: 2),
                        ActiveFlag = c.Boolean(nullable: false),
                        Category_CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Category", t => t.Category_CategoryID, cascadeDelete: true)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.ProductImage",
                c => new
                    {
                        ProductImageID = c.Int(nullable: false, identity: true),
                        SequenceNumber = c.Short(nullable: false),
                        ImageData = c.Binary(nullable: false),
                        ImageMimeType = c.String(nullable: false),
                        Product_ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductImageID)
                .ForeignKey("dbo.Product", t => t.Product_ProductID, cascadeDelete: true)
                .Index(t => t.Product_ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImage", "Product_ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "Category_CategoryID", "dbo.Category");
            DropIndex("dbo.ProductImage", new[] { "Product_ProductID" });
            DropIndex("dbo.Product", new[] { "Category_CategoryID" });
            DropTable("dbo.ProductImage");
            DropTable("dbo.Product");
            DropTable("dbo.Category");
        }
    }
}
