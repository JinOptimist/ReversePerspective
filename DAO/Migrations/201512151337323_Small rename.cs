namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Smallrename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.HeroAdditionalInfo", name: "VisibleAfterThatParagraph_Id", newName: "VisibleAfterThatPhrase_Id");
            RenameIndex(table: "dbo.HeroAdditionalInfo", name: "IX_VisibleAfterThatParagraph_Id", newName: "IX_VisibleAfterThatPhrase_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.HeroAdditionalInfo", name: "IX_VisibleAfterThatPhrase_Id", newName: "IX_VisibleAfterThatParagraph_Id");
            RenameColumn(table: "dbo.HeroAdditionalInfo", name: "VisibleAfterThatPhrase_Id", newName: "VisibleAfterThatParagraph_Id");
        }
    }
}
