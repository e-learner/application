namespace ELearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToTeacherTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teacher", "Email", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teacher", "Email");
        }
    }
}
