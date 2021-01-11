namespace SupportTrackPRO.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whoohoo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupportTicket", "ReasonForSupport", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupportTicket", "ReasonForSupport");
        }
    }
}
