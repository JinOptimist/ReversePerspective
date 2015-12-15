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
            HeroName = phrase.Hero.Name;
        }

        public long Position { get; set; }

        public string Text { get; set; }

        public string HeroName { get; set; }
    }
}