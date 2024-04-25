using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using backend.Models.Enum;
using Microsoft.AspNetCore.Http.HttpResults;
using backend.Repository;
using backend.Utils;

namespace backend.DAL;

public class ContactRepository: BaseRepository, IContactRepository
{

    public ContactRepository(PhonebookContext context) : base(context) { }

    public async Task<List<ContactDTO>> GetAll()
    {
        List<ContactDTO> contacts = await _context.Contacts
            .Where(x => !x.Deleted)
            .Include(x => x.ContactType)
            .Include(x => x.PersonContact)
            .Include(x => x.PublicOrganizationContact)
            .Include(x => x.PrivateOrganizationContact)
            .Select(x => ContactAdditionalFields.ItemToDTO(x))
            .ToListAsync();
        return contacts;
    }

    public Contact? GetById(int id)
    {
        return _context.Contacts.Find(id);
    }

    public void InsertContact(Contact contact)
    {
        _context.Contacts.Add(contact);
        _context.SaveChanges();
    }

    public void UpdateContact(Contact contact)
    {
        var privateOrganizationContacts = _context.PrivateOrganizationContacts.FirstOrDefault(x => x.ContactId == contact.Id);
        var personContact = _context.PersonContacts.FirstOrDefault(x => x.ContactId == contact.Id);
        var publicOrganizationContacts = _context.PublicOrganizationContacts.FirstOrDefault(x => x.ContactId == contact.Id);

        if (personContact != null) _context.PersonContacts.Remove(personContact);
        if (privateOrganizationContacts != null) _context.PrivateOrganizationContacts.Remove(privateOrganizationContacts);
        if (publicOrganizationContacts != null) _context.PublicOrganizationContacts.Remove(publicOrganizationContacts);

        _context.SaveChanges();
        _context.Entry(contact).State = EntityState.Modified;
        //_context.Contacts.Update(contact);
        _context.SaveChanges();
    }

    public void DeleteContact(Contact contact)
    {
        contact.Deleted = true;
        _context.SaveChanges();
    }

}
