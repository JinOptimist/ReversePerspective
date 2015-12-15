namespace DAO.Model
{
    public class HeroAdditionalInfo : BaseModel
    {
        public virtual string Info { get; set; }

        public virtual Phrase VisibleAfterThatPhrase { get; set; }

        public virtual Hero Hero { get; set; }
    }
}