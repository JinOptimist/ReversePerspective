namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlphaOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Paragraphs", "Opus_Id", "dbo.Opus");
            DropIndex("dbo.Paragraphs", new[] { "Opus_Id" });
            CreateTable(
                "dbo.Hero",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Opus_Id = c.Long(nullable: false),
                        Scene_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Opus", t => t.Opus_Id, cascadeDelete: true)
                .ForeignKey("dbo.Scene", t => t.Scene_Id)
                .Index(t => t.Opus_Id)
                .Index(t => t.Scene_Id);
            
            CreateTable(
                "dbo.HeroAdditionalInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Info = c.String(),
                        VisibleAfterThatParagraph_Id = c.Long(),
                        Hero_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Phrase", t => t.VisibleAfterThatParagraph_Id)
                .ForeignKey("dbo.Hero", t => t.Hero_Id, cascadeDelete: true)
                .Index(t => t.VisibleAfterThatParagraph_Id)
                .Index(t => t.Hero_Id);
            
            CreateTable(
                "dbo.Phrase",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Position = c.Long(nullable: false),
                        Text = c.String(),
                        Hero_Id = c.Long(),
                        Scene_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hero", t => t.Hero_Id)
                .ForeignKey("dbo.Scene", t => t.Scene_Id, cascadeDelete: true)
                .Index(t => t.Hero_Id)
                .Index(t => t.Scene_Id);
            
            CreateTable(
                "dbo.Scene",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        Opus_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Opus", t => t.Opus_Id, cascadeDelete: true)
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
            
            DropForeignKey("dbo.Hero", "Scene_Id", "dbo.Scene");
            DropForeignKey("dbo.HeroAdditionalInfo", "Hero_Id", "dbo.Hero");
            DropForeignKey("dbo.HeroAdditionalInfo", "VisibleAfterThatParagraph_Id", "dbo.Phrase");
            DropForeignKey("dbo.Phrase", "Scene_Id", "dbo.Scene");
            DropForeignKey("dbo.Scene", "Opus_Id", "dbo.Opus");
            DropForeignKey("dbo.Hero", "Opus_Id", "dbo.Opus");
            DropForeignKey("dbo.Phrase", "Hero_Id", "dbo.Hero");
            DropIndex("dbo.Scene", new[] { "Opus_Id" });
            DropIndex("dbo.Phrase", new[] { "Scene_Id" });
            DropIndex("dbo.Phrase", new[] { "Hero_Id" });
            DropIndex("dbo.HeroAdditionalInfo", new[] { "Hero_Id" });
            DropIndex("dbo.HeroAdditionalInfo", new[] { "VisibleAfterThatParagraph_Id" });
            DropIndex("dbo.Hero", new[] { "Scene_Id" });
            DropIndex("dbo.Hero", new[] { "Opus_Id" });
            DropTable("dbo.Scene");
            DropTable("dbo.Phrase");
            DropTable("dbo.HeroAdditionalInfo");
            DropTable("dbo.Hero");
            CreateIndex("dbo.Paragraphs", "Opus_Id");
            AddForeignKey("dbo.Paragraphs", "Opus_Id", "dbo.Opus", "Id");
        }
    }
}
