using System.Collections.Generic;

namespace DAO.Model
{
    public class Hero : BaseModel
    {
        public virtual string Name { get; set; }

        public virtual List<HeroAdditionalInfo> HeroInfo { get; set; }

        public virtual Scene Scene { get; set; }
    }
}