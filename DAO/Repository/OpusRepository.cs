using DAO.Model;

namespace DAO.Repository
{
    public class BaseRepository : BaseRepository<Opus>
    {
        public BaseRepository(ReversePerspectiveContext context) : base(context)
        {
        }
    }
}