namespace ELearnerAppDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBankAccountTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Deposit = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccounts", "AccountId", "dbo.Accounts");
            DropIndex("dbo.BankAccounts", new[] { "AccountId" });
            DropTable("dbo.BankAccounts");
        }
    }
}
