namespace ELearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeNameContentsToContentsPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "ContentsPath", c => c.String(nullable: false, maxLength: 1000, unicode: false));
            DropColumn("dbo.Courses", "Contents");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Contents", c => c.String(nullable: false, maxLength: 1000, unicode: false));
            DropColumn("dbo.Courses", "ContentsPath");
        }
    }
}
