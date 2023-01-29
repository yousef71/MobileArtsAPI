
using MobileArts.api.Persistence.Contexts;

namespace Dentrroper.Mobile.api.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDataContext _context;

        public BaseRepository(AppDataContext context)
        {
            _context = context;
        }
    }
}
