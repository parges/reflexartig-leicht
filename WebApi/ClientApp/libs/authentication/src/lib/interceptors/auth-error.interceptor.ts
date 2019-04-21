import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticationConfig } from '../interfaces';
import { AuthService } from '../services';
import { ModuleConfigToken } from '../token';

@Injectable()
export class AuthErrorInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService,
              @Inject(ModuleConfigToken) private authConfig: AuthenticationConfig) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        catchError(err => {
          if ((this.authConfig.authenticationErrors || [401, 403]).indexOf(err.status) !== -1) {
            // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
            this.auth.logout();
            location.reload();
          }

          const error = err.error.message || err.statusText;
          return throwError(error);
        })
      );
  }
}
