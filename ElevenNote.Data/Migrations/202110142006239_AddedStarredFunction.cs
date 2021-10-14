namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStarredFunction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "isStarred", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Note", "isStarred");
        }
    }
}
