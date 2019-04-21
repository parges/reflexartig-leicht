import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthService } from '../services';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router,
              private auth: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    : Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.auth.currentUser$
      .pipe(
        // map((currentUser: LoginResponse) => {
        map((currentUser: any) => {
          if (currentUser && currentUser.token) {
            // authorised so return true
            return true;
          }

          // redirect to login
          return this.router.createUrlTree(['/login'],
            !state.url
              ? {}
              : {
                queryParams: { returnUrl: state.url }
              });
        })
      );
  }
}
