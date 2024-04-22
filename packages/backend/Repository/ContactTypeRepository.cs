using backend.DAL;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class ContactTypeRepository : BaseRepository, IContactTypeRepository
{

    public ContactTypeRepository(PhonebookContext context) : base(context) { }

    public async Task<ContactType?> GetById(int id)
    {
        return await _context.ContactTypes.FindAsync(id);
    }
}
