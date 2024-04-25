import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { faEdit } from '@fortawesome/free-solid-svg-icons';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

import { ApiBaseUrl } from '@utils/config';
import type { ContactDTO } from '@interfaces/contact';

@Component({
  selector: 'app-phone-book',
  templateUrl: './phone-book.component.html'
})
export class PhoneBookComponent {
  editIcon = faEdit;
  deleteIcon = faTrash;
  contactsUrl = 'https://localhost:7142/'

  contacts: ContactDTO[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchContacts();
  }

  fetchContacts(): void {
    this.http.get<ContactDTO[]>(`${ApiBaseUrl}/contact/list`)
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

  openAddModal() {
    
  }

}
