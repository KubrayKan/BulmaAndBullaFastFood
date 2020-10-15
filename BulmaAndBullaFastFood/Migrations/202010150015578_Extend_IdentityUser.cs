namespace BulmaAndBullaFastFood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Extend_IdentityUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "City", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "City");
        }
    }
}
