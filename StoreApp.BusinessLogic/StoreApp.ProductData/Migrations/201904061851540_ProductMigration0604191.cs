namespace StoreApp.ProductData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductMigration0604191 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Product", name: "Category_CategoryID", newName: "CategoryID");
            RenameIndex(table: "dbo.Product", name: "IX_Category_CategoryID", newName: "IX_CategoryID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Product", name: "IX_CategoryID", newName: "IX_Category_CategoryID");
            RenameColumn(table: "dbo.Product", name: "CategoryID", newName: "Category_CategoryID");
        }
    }
}
