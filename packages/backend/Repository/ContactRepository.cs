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

namespace backend.DAL;

public class ContactRepository: BaseRepository, IContactRepository
{

    public ContactRepository(PhonebookContext context) : base(context) { }

    private static ContactDTO ItemToDTO(Contact contact)
    {
        return new ContactDTO
        {
            Id = contact.Id,
            Name = contact.Name,
            PhoneNumber = contact.PhoneNumber,
            TextComments = contact.TextComments ?? string.Empty,
            ContactTypeId = contact.ContactType.Id,
            AdditionalData = new AdditionalDataDTO
            {
                Email = contact.PersonContact?.Email,
                Relationship = contact.PersonContact?.Relationship,
                IndustrialSector = contact.PublicOrganizationContact?.IndustrialSector,
                WebpageUrl = contact.PublicOrganizationContact?.WebpageUrl,
                Fax = contact.PrivateOrganizationContact?.Fax,
                OfficeAddress = contact.PrivateOrganizationContact?.OfficeAddress,
            }
        };
    }

    public async Task<List<ContactDTO>> GetAll()
    {
        List<ContactDTO> contacts = await _context.Contacts
            .Select(x => ItemToDTO(x))
            .ToListAsync();
        return contacts;
    }

    public async Task<Contact?> GetById(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public void InsertContact(Contact contact)
    {
        _context.Contacts.Add(contact);
        Save();
    }

    public void UpdateContact(Contact contact)
    {
        _context.Entry(contact).State = EntityState.Modified;
        Save();
    }

    public void DeleteContact(Contact contact)
    {
        _context.Contacts.Remove(contact);
        Save();
    }

}
