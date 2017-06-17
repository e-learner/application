namespace ElearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankAccounts", "AccountId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BankAccounts", "AccountId", c => c.Int(nullable: false, identity: true));
        }
    }
}
