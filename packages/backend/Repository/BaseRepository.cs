using backend.Models;

namespace backend.Repository;

public class BaseRepository : IDisposable
{
    protected PhonebookContext _context;
    private bool _disposed = false;

    public BaseRepository(PhonebookContext context)
    {
        this._context = context;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async void Save()
    {
        await _context.SaveChangesAsync();
    }
}
