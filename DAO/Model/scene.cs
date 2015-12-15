using System.Collections.Generic;

namespace DAO.Model
{
    public class Scene : BaseModel
    {
        public virtual string Description { get; set; }

        public virtual List<Phrase> Phrases { get; set; }
    }
}