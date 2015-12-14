namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initproject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Opus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Paragraphs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        Position = c.Long(nullable: false),
                        Text = c.String(),
                        Opus_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Opus", t => t.Opus_Id)
                .Index(t => t.Opus_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paragraphs", "Opus_Id", "dbo.Opus");
            DropIndex("dbo.Paragraphs", new[] { "Opus_Id" });
            DropTable("dbo.Paragraphs");
            DropTable("dbo.Opus");
        }
    }
}
