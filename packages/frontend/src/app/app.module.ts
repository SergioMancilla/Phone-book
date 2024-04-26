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
import { AuthFormComponent } from './components/auth-form/auth-form.component';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    PhoneBookComponent,
    AddContactFormComponent,
    NgbdModalComponent,
    AuthFormComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    RouterOutlet
  ],
  providers: [],
  exports: [
    AppComponent,
    PhoneBookComponent,
    AddContactFormComponent,
    NgbdModalComponent,
    AuthFormComponent,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
