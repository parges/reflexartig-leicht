import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';

import { AuthService } from '../services';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService) { }

  /**
   * Append the token - if available - to the request headers.
   */
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return this.auth.currentUser$
      .pipe(
        // switchMap((currentUser: LoginResponse) => {
        switchMap((currentUser: any) => {

          if (currentUser && currentUser.token) {
            request = request.clone({
              setHeaders: {
                Authorization: `Bearer ${currentUser.token}`
              }
            });
          }

          return next.handle(request);
        })
      );
  }
}
