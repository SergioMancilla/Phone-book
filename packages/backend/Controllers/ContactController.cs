﻿using backend.DAL;
using backend.Models;
using backend.Models.DTO;
using backend.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Utils;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.AspNetCore.Cors;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private IContactRepository _contactRepository;
    private IContactTypeRepository _contactTypeRepository;

    public ContactController(IContactRepository contactRepository, IContactTypeRepository contactTypeRepository)
    {
        _contactRepository = contactRepository;
        _contactTypeRepository = contactTypeRepository;
    }

    [HttpGet()]
    public async Task<List<ContactDTO>> GetAll(string? filter = null)
    {

        if (!string.IsNullOrEmpty(filter))
        {
            var filterTypes = filter.Split('_').Select(int.Parse).ToList();
            return await _contactRepository.GetByTypes(filterTypes);
        } else
        {
            return await _contactRepository.GetAll();
        }
    }

    [HttpPost()]
    public async Task<ActionResult<ContactDTO>> Post(ContactDTO contactDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var contactType = await _contactTypeRepository.GetById(contactDto.ContactTypeId);

        if (contactType == null)
        {
            return BadRequest();
        }

        var contact = new Contact
        {
            Name = contactDto.Name,
            PhoneNumber = contactDto.PhoneNumber,
            ContactType = contactType,  
            ContactTypeId = contactDto.ContactTypeId,
            TextComments = contactDto.TextComments,
        };

        ContactAdditionalFields.modifyAdditionalFields(contactType, contact, contactDto);

        try
        {
            _contactRepository.InsertContact(contact);
            contactDto.Id = contact.Id;
            return CreatedAtAction(nameof(Post), new { id = contactDto.Id }, contactDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "There was an error in the server");
        }

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ContactDTO>> Put(int id, ContactDTO contactDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        contactDto.Id = id;
        var contactType = await _contactTypeRepository.GetById(contactDto.ContactTypeId);
        if (contactType == null)
        {
            return BadRequest();
        }

        var contact = _contactRepository.GetById(id);
        if (contact == null)
        {
            return NotFound();
        }

        contact.Name = contactDto.Name;
        contact.PhoneNumber = contactDto.PhoneNumber;
        contact.TextComments = contactDto.TextComments;
        contact.ContactType = contactType;

        ContactAdditionalFields.modifyAdditionalFields(contactType, contact, contactDto);

        try
        {
            _contactRepository.UpdateContact(contact);
            var new_contact = _contactRepository.GetById(contact.Id);
            return Ok(ContactAdditionalFields.ItemToDTO(new_contact!));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "There was an error in the server");
        }

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var contact = _contactRepository.GetById(id);
        Console.WriteLine($"Delete contact: {id}");
        if (contact == null)
        {
            return NotFound();
        }

        _contactRepository.DeleteContact(contact);

        return NoContent();
    }
}