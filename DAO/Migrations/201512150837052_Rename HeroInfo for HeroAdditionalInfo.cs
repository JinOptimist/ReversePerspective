namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameHeroInfoforHeroAdditionalInfo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.HeroInfoes", newName: "HeroAdditionalInfoes");
            DropColumn("dbo.HeroAdditionalInfoes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HeroAdditionalInfoes", "Name", c => c.String());
            RenameTable(name: "dbo.HeroAdditionalInfoes", newName: "HeroInfoes");
        }
    }
}
