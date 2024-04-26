import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { Router } from '@angular/router';

import { AuthService } from '@services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  if (AuthService.isLogged()) {
    inject(Router).navigate(['home']);
    return false;
  }

  return true;
};
