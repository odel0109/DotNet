namespace StoreApp.ProductData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductMigration060419 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductImage", newName: "ProductImages");
            RenameColumn(table: "dbo.ProductImages", name: "Product_ProductID", newName: "ProductID");
            RenameIndex(table: "dbo.ProductImages", name: "IX_Product_ProductID", newName: "IX_ProductID");
            DropPrimaryKey("dbo.ProductImages");
            AddPrimaryKey("dbo.ProductImages", new[] { "ProductID", "SequenceNumber" });
            DropColumn("dbo.ProductImages", "ProductImageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductImages", "ProductImageID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.ProductImages");
            AddPrimaryKey("dbo.ProductImages", "ProductImageID");
            RenameIndex(table: "dbo.ProductImages", name: "IX_ProductID", newName: "IX_Product_ProductID");
            RenameColumn(table: "dbo.ProductImages", name: "ProductID", newName: "Product_ProductID");
            RenameTable(name: "dbo.ProductImages", newName: "ProductImage");
        }
    }
}
