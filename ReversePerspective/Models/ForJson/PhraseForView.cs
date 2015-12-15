using DAO.Model;

namespace ReversePerspective.Models.ForJson
{
    public class PhraseForView : BaseModelForView
    {
        public PhraseForView(Phrase phrase)
            : base(phrase)
        {
            Position = phrase.Position;
            Text = phrase.Text;
            if (phrase.Hero != null)
            {
                HeroName = phrase.Hero.Name;
                HeroId = phrase.Hero.Id;
            }
        }

        public long Position { get; set; }

        public string Text { get; set; }

        public string HeroName { get; set; }

        public long HeroId { get; set; }
    }
}