using DAO.Model;

namespace DAO.Repository
{
    public class OpusRepository : BaseRepository<Opus>
    {
        public OpusRepository(ReversePerspectiveContext context) : base(context)
        {
        }
    }
}