using System.Collections.Generic;
using System.Linq;
using DAO.Model;

namespace DAO.Repository
{
    public class HeroInfoRepository : BaseRepository<HeroAdditionalInfo>
    {
        public HeroInfoRepository(ReversePerspectiveContext context)
            : base(context)
        {
        }

        public List<HeroAdditionalInfo> GetByHero(long heroId)
        {
            return Db.HeroAdditionalInfo.Where(x => x.Hero.Id == heroId).ToList();
        }
    }
}