using DAO.Model;

namespace ReversePerspective.Models.ForJson
{
    public class HeroInfoForView : BaseModelForView
    {
        public HeroInfoForView(HeroAdditionalInfo heroAdditionalInfo)
            : base(heroAdditionalInfo)
        {
            Info = heroAdditionalInfo.Info;
            VisibleAfterThatParagraphId = heroAdditionalInfo.VisibleAfterThatParagraph.Id;
        }

        public string Info { get; set; }

        public long VisibleAfterThatParagraphId { get; set; }
    }
}