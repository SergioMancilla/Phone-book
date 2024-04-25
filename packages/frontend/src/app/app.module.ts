import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PhoneBookComponent } from '@components/phone-book/phone-book.component';
import { AddContactFormComponent } from './components/add-contact-form/contact-form.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalComponent } from './components/ngbd-modal/ngbd-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    PhoneBookComponent,
    AddContactFormComponent,
    NgbdModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
