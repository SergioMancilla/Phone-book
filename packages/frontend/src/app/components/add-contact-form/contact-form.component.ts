import { Component, inject, Input } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { ContactType } from '@interfaces/contact';
import type { ContactDTO, Mode } from '@interfaces/contact'
import { ApiBaseUrl } from '@utils/config';

@Component({
  selector: 'app-add-contact-form',
  templateUrl: './contact-form.component.html',
})
export class AddContactFormComponent {
  activeModal = inject(NgbActiveModal);

  @Input() mode: Mode = 'create';
  @Input() contact: ContactDTO = {} as ContactDTO;

  selectedContactType: ContactType | null = null;
  ContactType = ContactType;
  contactForm: FormGroup | null = null;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.contactForm = new FormGroup({
      id: new FormControl(this.contact?.id),
      name: new FormControl(this.contact?.name),
      phone: new FormControl(this.contact?.phone_number),
      comments: new FormControl(this.contact?.text_comments),
      contactType: new FormControl(this.contact?.contact_type_id),
      relationship: new FormControl(this.contact?.additional_data?.relationship),
      email: new FormControl(this.contact?.additional_data?.email),
      address: new FormControl(this.contact?.additional_data?.office_address),
      fax: new FormControl(this.contact?.additional_data?.fax),
      webpage: new FormControl(this.contact?.additional_data?.webpage_url),
      industrialSector: new FormControl(this.contact?.additional_data?.industrial_sector),
    })

    this.selectedContactType = this.contact?.contact_type_id;
  }

  onContactTypeChange(event: Event) {
    const id = +(event.target as HTMLInputElement).value;
    this.selectedContactType = id;
  }

  onSubmit(form: NgForm) {
    const values = form.value;

    const contactDTO: ContactDTO = {
      name: values.name,
      phone_number: values.phone_number,
      text_comments: values.text_comments,
      contact_type_id: values.contact_type_id,
      additional_data: {
        relationship: values.relationship,
        email: values.email,
        office_address: values.office_address,
        fax: values.fax,
        webpage_url: values.webpage_url,
        industrial_sector: values.industrial_sector
      }
    }

    if (this.mode === 'create') {
      this.saveContact(contactDTO);
    } else {
      this.updateContact(this.contact.id!, contactDTO);
    }
    
  }

  updateContact(contactId: string, contactDTO: ContactDTO) {
    this.http.put(`${ApiBaseUrl}/contact/${contactId}`, contactDTO)
      .subscribe({
        next: () => {
          this.activeModal.close('Contact updated');
        },
        error: (e) => console.log("There was an error in the request: ", e),
        complete: () => { this.activeModal.dismiss() }
      });
  }

  saveContact(contactDTO: ContactDTO) {
    this.http.post(`${ApiBaseUrl}/contact`, contactDTO)
    .subscribe({
      next: () => {},
      error: (e) => console.log("The was an error in the request: ", e),
      complete: () => { this.activeModal.dismiss() }
    })
  }
}
