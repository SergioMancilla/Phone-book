using System;
using System.Collections.Generic;
using backend.Models;
using backend.Models.DTO;

namespace backend.DAL;

public interface IContactRepository : IDisposable
{
    Task<List<ContactDTO>> GetAll();
    Task<Contact> GetById(int id);
    void InsertContact(ContactDTO contactDto);
    void UpdateContact(Contact contact);
    void DeleteContact(int id);
    void Save();
}
