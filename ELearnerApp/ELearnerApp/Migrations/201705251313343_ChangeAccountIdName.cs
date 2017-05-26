namespace ELearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAccountIdName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BankAccounts", name: "AccoundId", newName: "AccountId");
            RenameIndex(table: "dbo.BankAccounts", name: "IX_AccoundId", newName: "IX_AccountId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BankAccounts", name: "IX_AccountId", newName: "IX_AccoundId");
            RenameColumn(table: "dbo.BankAccounts", name: "AccountId", newName: "AccoundId");
        }
    }
}
