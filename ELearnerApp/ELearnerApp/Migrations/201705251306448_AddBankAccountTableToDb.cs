namespace ELearnerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBankAccountTableToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Deposit = c.Decimal(nullable: false, precision: 10, scale: 2),
                        AccoundId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccoundId, cascadeDelete: true)
                .Index(t => t.AccoundId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccounts", "AccoundId", "dbo.Accounts");
            DropIndex("dbo.BankAccounts", new[] { "AccoundId" });
            DropTable("dbo.BankAccounts");
        }
    }
}
