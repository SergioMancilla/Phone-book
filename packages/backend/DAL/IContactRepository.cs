using System;
using System.Collections.Generic;
using backend.Models;
using backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace backend.DAL;

public interface IContactRepository : IDisposable
{
    Task<List<ContactDTO>> GetAll();
    Task<List<ContactDTO>> GetByTypes(List<int> typeIds);
    Contact? GetById(int id);
    void InsertContact(Contact contact);
    void UpdateContact(Contact contact);
    void DeleteContact(Contact contact);
}
