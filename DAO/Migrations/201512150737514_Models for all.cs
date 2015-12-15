namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modelsforall : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Paragraphs", "Opus_Id", "dbo.Opus");
            DropIndex("dbo.Paragraphs", new[] { "Opus_Id" });
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Scene_Id = c.Long(),
                        Opus_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scenes", t => t.Scene_Id)
                .ForeignKey("dbo.Opus", t => t.Opus_Id)
                .Index(t => t.Scene_Id)
                .Index(t => t.Opus_Id);
            
            CreateTable(
                "dbo.HeroInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Info = c.String(),
                        Hero_Id = c.Long(),
                        VisibleAfterThatParagraph_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Heroes", t => t.Hero_Id)
                .ForeignKey("dbo.Phrases", t => t.VisibleAfterThatParagraph_Id)
                .Index(t => t.Hero_Id)
                .Index(t => t.VisibleAfterThatParagraph_Id);
            
            CreateTable(
                "dbo.Phrases",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Position = c.Long(nullable: false),
                        Text = c.String(),
                        Hero_Id = c.Long(),
                        Scene_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Heroes", t => t.Hero_Id)
                .ForeignKey("dbo.Scenes", t => t.Scene_Id)
                .Index(t => t.Hero_Id)
                .Index(t => t.Scene_Id);
            
            CreateTable(
                "dbo.Scenes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        Opus_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Opus", t => t.Opus_Id)
                .Index(t => t.Opus_Id);
            
            DropTable("dbo.Paragraphs");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Scenes", "Opus_Id", "dbo.Opus");
            DropForeignKey("dbo.Heroes", "Opus_Id", "dbo.Opus");
            DropForeignKey("dbo.Heroes", "Scene_Id", "dbo.Scenes");
            DropForeignKey("dbo.HeroInfoes", "VisibleAfterThatParagraph_Id", "dbo.Phrases");
            DropForeignKey("dbo.Phrases", "Scene_Id", "dbo.Scenes");
            DropForeignKey("dbo.Phrases", "Hero_Id", "dbo.Heroes");
            DropForeignKey("dbo.HeroInfoes", "Hero_Id", "dbo.Heroes");
            DropIndex("dbo.Scenes", new[] { "Opus_Id" });
            DropIndex("dbo.Phrases", new[] { "Scene_Id" });
            DropIndex("dbo.Phrases", new[] { "Hero_Id" });
            DropIndex("dbo.HeroInfoes", new[] { "VisibleAfterThatParagraph_Id" });
            DropIndex("dbo.HeroInfoes", new[] { "Hero_Id" });
            DropIndex("dbo.Heroes", new[] { "Opus_Id" });
            DropIndex("dbo.Heroes", new[] { "Scene_Id" });
            DropTable("dbo.Scenes");
            DropTable("dbo.Phrases");
            DropTable("dbo.HeroInfoes");
            DropTable("dbo.Heroes");
            CreateIndex("dbo.Paragraphs", "Opus_Id");
            AddForeignKey("dbo.Paragraphs", "Opus_Id", "dbo.Opus", "Id");
        }
    }
}
