namespace ExpenseTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BudgetCategories",
                c => new
                {
                    BudgetCategoryId = c.Int(nullable: false, identity: true),
                    Category = c.String(),
                    AllocatedPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                    BudgetId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.BudgetCategoryId)
                .ForeignKey("dbo.Budgets", t => t.BudgetId, cascadeDelete: false) // Changed to false
                .Index(t => t.BudgetId);

            CreateTable(
                "dbo.Budgets",
                c => new
                {
                    BudgetId = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    UserId = c.Int(nullable: false),
                    MonthlyIncome = c.Decimal(nullable: false, precision: 18, scale: 2),
                    StartDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.BudgetId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false) // Changed to false
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    Username = c.String(),
                    Password = c.String(),
                    Email = c.String(),
                })
                .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.Expenses",
                c => new
                {
                    ExpenseId = c.Int(nullable: false, identity: true),
                    Date = c.DateTime(nullable: false),
                    Category = c.String(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Description = c.String(nullable: false),
                    UserId = c.Int(nullable: false),
                    BudgetId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ExpenseId)
                .ForeignKey("dbo.Budgets", t => t.BudgetId, cascadeDelete: false) // Changed to false
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false) // Changed to false
                .Index(t => t.UserId)
                .Index(t => t.BudgetId);
        }


        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "UserId", "dbo.Users");
            DropForeignKey("dbo.Expenses", "BudgetId", "dbo.Budgets");
            DropForeignKey("dbo.Budgets", "UserId", "dbo.Users");
            DropForeignKey("dbo.BudgetCategories", "BudgetId", "dbo.Budgets");
            DropIndex("dbo.Expenses", new[] { "BudgetId" });
            DropIndex("dbo.Expenses", new[] { "UserId" });
            DropIndex("dbo.Budgets", new[] { "UserId" });
            DropIndex("dbo.BudgetCategories", new[] { "BudgetId" });
            DropTable("dbo.Expenses");
            DropTable("dbo.Users");
            DropTable("dbo.Budgets");
            DropTable("dbo.BudgetCategories");
        }
    }
}
