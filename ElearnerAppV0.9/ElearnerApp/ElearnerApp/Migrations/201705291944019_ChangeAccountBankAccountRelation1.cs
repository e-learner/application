namespace ElearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAccountBankAccountRelation1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BankAccounts", new[] { "AccountId" });
            DropPrimaryKey("dbo.BankAccounts");
            AlterColumn("dbo.BankAccounts", "AccountId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BankAccounts", "AccountId");
            CreateIndex("dbo.BankAccounts", "AccountId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BankAccounts", new[] { "AccountId" });
            DropPrimaryKey("dbo.BankAccounts");
            AlterColumn("dbo.BankAccounts", "AccountId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BankAccounts", "AccountId");
            CreateIndex("dbo.BankAccounts", "AccountId");
        }
    }
}
