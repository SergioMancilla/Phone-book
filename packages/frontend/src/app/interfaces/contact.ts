export type Mode = 'edit' | 'create';

export enum ContactType {
  PERSON = 1,
  PUBLIC_ORGANIZATION = 2,
  PRIVATE_ORGANIZATION = 3
}

export interface AdditionalDataDTO {
  relationship?: string,
  email?: string
  office_address: string
  fax: string
  webpage_url: URL,
  industrial_sector: string
}

export interface ContactDTO {
  id?: string,
  name: string,
  phone_number: string,
  text_comments?: string,
  contact_type_id: ContactType,
  additional_data: AdditionalDataDTO
}