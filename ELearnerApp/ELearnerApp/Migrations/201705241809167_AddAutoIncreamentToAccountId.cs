namespace ELearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAutoIncreamentToAccountId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Id", c => c.Int(nullable: false));
        }
    }
}
