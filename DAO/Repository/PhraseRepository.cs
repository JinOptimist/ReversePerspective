using DAO.Model;

namespace DAO.Repository
{
    public class PhraseRepository : BaseRepository<Phrase>
    {
        public PhraseRepository(ReversePerspectiveContext context) : base(context)
        {
        }
    }
}