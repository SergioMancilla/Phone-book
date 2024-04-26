import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  static isLogged (): boolean {
    const logged = localStorage.getItem('logged')
    if (!logged) return false;
    if (!!+logged) return true;
    return false;
  }
}
