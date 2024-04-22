using backend.Models;

namespace backend.DAL;

public interface IContactTypeRepository
{
    Task<ContactType?> GetById(int id);
}
