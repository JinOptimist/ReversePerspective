using DAO.Model;

namespace DAO.Repository
{
    public class SceneRepository : BaseRepository<Scene>
    {
        public SceneRepository(ReversePerspectiveContext context) : base(context)
        {
        }
    }
}