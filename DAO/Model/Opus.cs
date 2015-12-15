using System.Collections.Generic;

namespace DAO.Model
{
    public class Opus : BaseModel
    {
        public virtual string Name { get; set; }

        public virtual List<Scene> Scenes { get; set; }

        public virtual List<Hero> Heroes { get; set; }
    }
}