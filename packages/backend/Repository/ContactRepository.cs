using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.DAL;

public class ContactRepository : IContactRepository, IDisposable
{
    private PhonebookContext _context;
    private bool _disposed = false;

    public ContactRepository(PhonebookContext context)
    {
       this. _context = context;
    }

    public async Task<List<Contact>> GetAll()
    {
        List<Contact> contacts = await _context.Contacts.ToListAsync();
        return contacts;
    }

    public async Task<Contact> GetById(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public void InsertContact(Contact contact)
    {
        _context.Contacts.Add(contact);
    }
    public void UpdateContact(Contact contact)
    {
        _context.Entry(contact).State = EntityState.Modified;
    }

    public void DeleteContact(int id)
    {
        Contact contact = _context.Contacts.Find(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
        }
    }

    public async void Save()
    {
        await _context.SaveChangesAsync();
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

}
