using backend.DAL;
using backend.Models;
using backend.Models.DTO;
using backend.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Utils;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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

    [HttpGet("list")]
    public async Task<List<ContactDTO>> GetAll()
    {
        var contacts = await _contactRepository.GetAll();
        return contacts;
    }

    [HttpPost()]
    public async Task<ActionResult<ContactDTO>> Post(ContactDTO contactDto)
    {
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
        var contactType = await _contactTypeRepository.GetById(contactDto.ContactTypeId);
        if (id != contactDto.Id || contactType == null)
        {
            return BadRequest();
        }

        var contact = await _contactRepository.GetById(id);
        if (contact == null)
        {
            return NotFound();
        }

        contact.Name = contactDto.Name;
        contact.PhoneNumber = contactDto.PhoneNumber;
        contact.TextComments = contactDto.TextComments;
        contact.ContactType = contactType;

        contact.PersonContact = null;
        contact.PrivateOrganizationContact = null;
        contact.PublicOrganizationContact = null;

        ContactAdditionalFields.modifyAdditionalFields(contactType, contact, contactDto);

        try
        {
            _contactRepository.UpdateContact(contact);
            return Ok(contactDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "There was an error in the server");
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var contact = await _contactRepository.GetById(id);
        if (contact == null)
        {
            return NotFound();
        }

        _contactRepository.DeleteContact(contact);

        return NoContent();
    }
}