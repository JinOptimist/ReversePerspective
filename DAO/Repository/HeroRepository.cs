using System.Linq;
using DAO.Model;

namespace DAO.Repository
{
    public class HeroRepository : BaseRepository<Hero>
    {
        public HeroRepository(ReversePerspectiveContext context)
            : base(context)
        {
        }

        public Hero GetByName(string name, Scene scene)
        {
            return Db.Hero.SingleOrDefault(x => x.Name == name && x.Scene.Id == scene.Id);
        }
    }
}