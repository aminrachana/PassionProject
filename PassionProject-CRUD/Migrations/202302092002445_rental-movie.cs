namespace PassionProject_CRUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rentalmovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "MovieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "MovieId");
            AddForeignKey("dbo.Rentals", "MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "MovieId", "dbo.Movies");
            DropIndex("dbo.Rentals", new[] { "MovieId" });
            DropColumn("dbo.Rentals", "MovieId");
        }
    }
}
