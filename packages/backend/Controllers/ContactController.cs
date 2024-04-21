using backend.DAL;
using backend.Models;
using backend.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private IContactRepository _contactRepository;

    public ContactController(IContactRepository contactRepository)
    {
        this._contactRepository = contactRepository;
    }

    [HttpGet("list")]
    public async Task<List<ContactDTO>> GetAll()
    {
        var contacts = await _contactRepository.GetAll();
        return contacts;
    }

    [HttpPost()]
    public ActionResult<ContactDTO> Post(ContactDTO contactDto)
    {
        _contactRepository.InsertContact(contactDto);
        _contactRepository.Save();

        return CreatedAtAction(nameof(Post), new { id = contactDto.Id }, contactDto);
    }
}