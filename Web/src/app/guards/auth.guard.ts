import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

export const AuthGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem('auth_token');
  if (!token) {
    const router = inject(Router);
    router.navigate(['/login']);
    return false;
  }
  return true;
};
