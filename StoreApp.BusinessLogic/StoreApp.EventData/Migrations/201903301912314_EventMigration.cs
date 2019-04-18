namespace StoreApp.EventData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        DiscountID = c.Int(nullable: false, identity: true),
                        DiscountType = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TargetType = c.Short(nullable: false),
                        Target = c.Int(nullable: false),
                        MinQuantity = c.Int(),
                        Event_EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiscountID)
                .ForeignKey("dbo.Event", t => t.Event_EventID, cascadeDelete: true)
                .Index(t => t.Event_EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discount", "Event_EventID", "dbo.Event");
            DropIndex("dbo.Discount", new[] { "Event_EventID" });
            DropTable("dbo.Discount");
            DropTable("dbo.Event");
        }
    }
}
