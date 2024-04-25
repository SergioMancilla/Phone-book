import { Component, Input, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddContactFormComponent } from '@components/add-contact-form/contact-form.component';

import type { ContactDTO, Mode } from '@interfaces/contact';

@Component({
  selector: 'app-ngbd-modal',
  templateUrl: './ngbd-modal.component.html',
})
export class NgbdModalComponent {

  @Input() mode: Mode = 'create';
  @Input() contact: ContactDTO | null = null;
  private modalService = inject(NgbModal);

  open() {
    console.log(this.contact)
		const modalRef = this.modalService.open(AddContactFormComponent);
		modalRef.componentInstance.name = 'ContactForm';
    modalRef.componentInstance.mode = this.mode;
    modalRef.componentInstance.contact = this.contact;
	}
  
}
