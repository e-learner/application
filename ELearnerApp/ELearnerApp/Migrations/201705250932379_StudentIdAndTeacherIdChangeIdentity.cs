namespace ELearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentIdAndTeacherIdChangeIdentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Teachers", "Id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teachers", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "Id", c => c.Int(nullable: false));
        }
    }
}
