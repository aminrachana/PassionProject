namespace PassionProject_CRUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rentals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        RentalId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PuchaseDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RentalId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rentals");
        }
    }
}
