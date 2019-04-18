namespace StoreApp.LanguageData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LanguageMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageDetail",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        LanguageCode = c.Short(nullable: false),
                        Message = c.String(maxLength: 2048),
                    })
                .PrimaryKey(t => new { t.MessageID, t.LanguageCode });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MessageDetail");
        }
    }
}
