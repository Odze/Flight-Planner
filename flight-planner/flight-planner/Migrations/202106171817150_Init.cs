namespace flight_planner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        country = c.String(),
                        city = c.String(),
                        airport = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        carrier = c.String(),
                        departureTime = c.String(),
                        arrivalTime = c.String(),
                        from_Id = c.Int(),
                        to_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Airports", t => t.from_Id)
                .ForeignKey("dbo.Airports", t => t.to_Id)
                .Index(t => t.from_Id)
                .Index(t => t.to_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "to_Id", "dbo.Airports");
            DropForeignKey("dbo.Flights", "from_Id", "dbo.Airports");
            DropIndex("dbo.Flights", new[] { "to_Id" });
            DropIndex("dbo.Flights", new[] { "from_Id" });
            DropTable("dbo.Flights");
            DropTable("dbo.Airports");
        }
    }
}
