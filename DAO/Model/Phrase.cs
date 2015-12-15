namespace DAO.Model
{
    public class Phrase : BaseModel
    {
        public virtual long Position { get; set; }

        public virtual string Text { get; set; }

        public virtual Hero Hero { get; set; }

        public virtual Scene Scene { get; set; }
    }
}