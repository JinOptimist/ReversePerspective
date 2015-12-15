using System.Linq;
using DAO.Model;

namespace DAO.Repository
{
    public class HeroRepository : OpusRepository<Hero>
    {
        public Hero GetByName(string name, Scene scene)
        {
            return _db.Hero.SingleOrDefault(x => x.Name == name && x.Scene.Id == scene.Id);
        }
    }
}