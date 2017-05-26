namespace ELearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStudentTableProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankAccounts", "AccountId", "dbo.Accounts");
            DropIndex("dbo.BankAccounts", new[] { "AccountId" });
            DropTable("dbo.BankAccounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Deposit = c.Decimal(nullable: false, precision: 10, scale: 2),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.BankAccounts", "AccountId");
            AddForeignKey("dbo.BankAccounts", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
    }
}
