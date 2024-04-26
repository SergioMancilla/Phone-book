import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { faEdit } from '@fortawesome/free-solid-svg-icons';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

import { ApiBaseUrl } from '@utils/config';
import { ContactDTO, ContactType } from '@interfaces/contact';

@Component({
  selector: 'app-phone-book',
  templateUrl: './phone-book.component.html'
})
export class PhoneBookComponent {
  editIcon = faEdit;
  deleteIcon = faTrash;
  contactsUrl = 'https://localhost:7142/'

  contacts: ContactDTO[] = [];

  fetchPersonContact: boolean = true;
  fetchPublicContact: boolean = true;
  fetchPrivateContact: boolean = true;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchContacts();
  }

  formatQueryFilterByType(): string {
    if (!this.fetchPersonContact && !this.fetchPrivateContact && !this.fetchPublicContact ) {
      return ""
    }

    let query = '?query='
    if (this.fetchPersonContact) query += `_${ContactType.PERSON}`
    if (this.fetchPrivateContact) query += `_${ContactType.PRIVATE_ORGANIZATION}`
    if (this.fetchPublicContact) query += `_${ContactType.PUBLIC_ORGANIZATION}`

    return query;
  }

  fetchContacts(): void {
    const query = this.formatQueryFilterByType();
    this.http.get<ContactDTO[]>(`${ApiBaseUrl}/contact${query}`)
    .subscribe({
      next: (data: ContactDTO[]) => {console.log(data); this.contacts = data},
      error: (e) => console.log("The was an error in the request: ", e),
      complete: () => {}
    })
  }

  deleteContact(contactId: string): void {
    this.http.delete(`${ApiBaseUrl}/contact/${contactId}`)
      .subscribe({
        next: () => {
          this.contacts = this.contacts.filter(contact => contact.id !== contactId);
          console.log('Contact deleted successfully.');
        },
        error: (error) => {
          console.error('Error deleting contact:', error);
        }
      });
  }

}
