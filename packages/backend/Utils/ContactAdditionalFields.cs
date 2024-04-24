using backend.Models.DTO;
using backend.Models.Enum;
using backend.Models;
using System;

namespace backend.Utils;

public class ContactAdditionalFields
{
    public static void modifyAdditionalFields(ContactType contactType, Contact contact, ContactDTO contactDto)
    {
        switch (contactType.Id)
        {
            case (int)ContactTypeEnum.Person:
                contact.PersonContact = new PersonContact
                {
                    Email = contactDto.AdditionalData?.Email ?? string.Empty,
                    Relationship = contactDto.AdditionalData?.Relationship ?? string.Empty
                };
                break;
            case (int)ContactTypeEnum.PublicOrganization:
                contact.PublicOrganizationContact = new PublicOrganizationContact
                {
                    IndustrialSector = contactDto.AdditionalData?.IndustrialSector ?? string.Empty,
                    WebpageUrl = contactDto.AdditionalData?.WebpageUrl ?? string.Empty,
                };
                break;
            case (int)ContactTypeEnum.PrivateOrganization:
                contact.PrivateOrganizationContact = new PrivateOrganizationContact
                {
                    Fax = contactDto.AdditionalData?.Fax ?? string.Empty,
                    OfficeAddress = contactDto.AdditionalData?.OfficeAddress ?? string.Empty,
                };
                break;
            default:
                throw new Exception("Invalid contact type");
        }
    }

    public static ContactDTO ItemToDTO(Contact contact)
    {

        return new ContactDTO
        {
            Id = contact.Id,
            Name = contact.Name,
            PhoneNumber = contact.PhoneNumber,
            TextComments = contact.TextComments ?? string.Empty,
            ContactTypeId = contact.ContactType?.Id ?? 0,
            AdditionalData = new AdditionalDataDTO
            {
                Email = contact.PersonContact?.Email ?? string.Empty,
                Relationship = contact.PersonContact?.Relationship ?? string.Empty,
                IndustrialSector = contact.PublicOrganizationContact?.IndustrialSector ?? string.Empty,
                WebpageUrl = contact.PublicOrganizationContact?.WebpageUrl ?? string.Empty,
                Fax = contact.PrivateOrganizationContact?.Fax ?? string.Empty,
                OfficeAddress = contact.PrivateOrganizationContact?.OfficeAddress ?? string.Empty,
            }
        };
    }

}