import { HttpClient } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { RouterLink, Router } from '@angular/router';

import { ApiBaseUrl } from '@utils/config';

type AuthMode = 'login' | 'register'

interface AuthFormControls {
  name?: FormControl,
  email: FormControl,
  password: FormControl
}

interface LoginDTO {
  email: string,
  password: string,
}

interface RegisterDTO extends LoginDTO {
  name: string
}

@Component({
  selector: 'app-auth-form',
  templateUrl: './auth-form.component.html',
})
export class AuthFormComponent {
  @Input() mode: AuthMode = 'login';

  authForm: FormGroup | null = null;

  name: string = '';
  email: string = '';
  password: string = '';

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    const emailControl = new FormControl(this.email, [Validators.required, Validators.email])
    const passwordControl = new FormControl(this.password, [Validators.required, Validators.minLength(8)])
    const nameControl = new FormControl(this.name, [Validators.required])

    let formControls: AuthFormControls = {
      email: emailControl,
      password: passwordControl
    }

    if (this.mode === 'register') {
      formControls.name = nameControl
    }

    this.authForm = new FormGroup(formControls)
  }

  onSubmit(form: NgForm) {
    if (!form.valid) {
      return
    }
    const values = form.value;
    
    if (this.mode === 'login') {
      this.doLogin({
        email: values.email,
        password: values.passwords
      })
    } else {
      this.doRegister({
        name: values.name,
        email: values.email,
        password: values.passwords
      })
    }

  }

  doLogin(loginDTO: LoginDTO) {
    this.http.post(`${ApiBaseUrl}/auth/login`, loginDTO)
      .subscribe({
        next: (response) => {
          if (response.status == 200) {
            this.saveSessionInStorage();
            this.router.navigate(['home']);
          }
        },
        error: (e) => {
          console.log("There was an error in the request: ", e)
        },
        complete: () => { }
      });
  }

  doRegister(registerDTO: RegisterDTO) {
    this.http.post(`${ApiBaseUrl}/auth/register`, registerDTO)
    .subscribe({
      next: (response) => {
        if (response.status == 200) {
          this.saveSessionInStorage();
          this.router.navigate(['home']);
        }
      },
      error: (e) => {
        console.log("The was an error in the request: ", e)
      },
      complete: () => { }
    })
  }

  saveSessionInStorage() {
    /* This is just to emulate a login, actually didn't implement some
    like a token based system or JWT, and any person could alter
    the login functionallity by opening the dev tools
    */
   localStorage.setItem('logged', '1');
  }
}
