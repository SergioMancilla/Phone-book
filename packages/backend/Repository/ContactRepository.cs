using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using backend.Models.Enum;

namespace backend.DAL;

public class ContactRepository : IContactRepository, IDisposable
{
    private PhonebookContext _context;
    private bool _disposed = false;

    private static ContactDTO ItemToDTO (Contact contact)
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

    public ContactRepository(PhonebookContext context)
    {
       this. _context = context;
    }

    public async Task<List<ContactDTO>> GetAll()
    {
        List<ContactDTO> contacts = await _context.Contacts
            .Select(x => ItemToDTO(x))
            .ToListAsync();
        return contacts;
    }

    public async Task<Contact> GetById(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public void InsertContact(ContactDTO contactDto)
    {
        try
        {
            var contactType = _context.ContactTypes.Find(contactDto.ContactTypeId);

            if (contactType == null)
            {
                throw new Exception("Contact type property must be specified");
            }

            var contact = new Contact
            {
                Name = contactDto.Name,
                PhoneNumber = contactDto.PhoneNumber,
                ContactType = contactType,
                ContactTypeId = contactDto.ContactTypeId,
                TextComments = contactDto.TextComments,
            };
           
            switch (contactType.Id)
            {
                case (int) ContactTypeEnum.Person:
                    contact.PersonContact = new PersonContact
                    {
                        Email = contactDto.AdditionalData?.Email ?? string.Empty,
                        Relationship = contactDto.AdditionalData?.Relationship ?? string.Empty
                    };
                    break;
                case (int) ContactTypeEnum.PublicOrganization:
                    contact.PublicOrganizationContact = new PublicOrganizationContact
                    {
                        IndustrialSector = contactDto.AdditionalData?.IndustrialSector ?? string.Empty,
                        WebpageUrl = contactDto.AdditionalData?.WebpageUrl ?? string.Empty,
                    };
                    break;
                case (int) ContactTypeEnum.PrivateOrganization:
                    contact.PrivateOrganizationContact = new PrivateOrganizationContact
                    {
                        Fax = contactDto.AdditionalData?.Fax ?? string.Empty,
                        OfficeAddress = contactDto.AdditionalData?.OfficeAddress ?? string.Empty,
                    };
                    break;
                default:
                    throw new Exception("Invalid contact type");
            }

        } catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        //_context.Contacts.Add(contact);
    }
    public void UpdateContact(Contact contact)
    {
        _context.Entry(contact).State = EntityState.Modified;
    }

    public void DeleteContact(int id)
    {
        var contact = _context.Contacts.Find(id);
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
