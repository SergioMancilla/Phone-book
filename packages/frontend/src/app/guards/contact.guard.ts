import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { Router } from '@angular/router';

import { AuthService } from '@services/auth.service';

export const contactGuard: CanActivateFn = (route, state) => {
  if (AuthService.isLogged()) {
    return true;
  }

  inject(Router).navigate(['login']);
  return false;
};
