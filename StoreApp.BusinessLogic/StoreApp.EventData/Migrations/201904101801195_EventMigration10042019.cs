namespace StoreApp.EventData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventMigration10042019 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Discount", name: "Event_EventID", newName: "EventID");
            RenameIndex(table: "dbo.Discount", name: "IX_Event_EventID", newName: "IX_EventID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Discount", name: "IX_EventID", newName: "IX_Event_EventID");
            RenameColumn(table: "dbo.Discount", name: "EventID", newName: "Event_EventID");
        }
    }
}
