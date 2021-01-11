namespace SupportTrackPRO.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yay : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SupportTicket", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SupportTicket", "Status", c => c.String(nullable: false));
        }
    }
}
