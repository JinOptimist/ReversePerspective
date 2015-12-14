using System.Collections.Generic;

namespace DAO.Model
{
    public class Opus : BaseModel
    {
        public virtual string Name { get; set; }

        public virtual List<Paragraph> Paragraphs { get; set; }
    }
}