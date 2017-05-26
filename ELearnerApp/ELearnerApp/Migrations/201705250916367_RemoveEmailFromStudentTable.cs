namespace ELearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEmailFromStudentTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Email", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
