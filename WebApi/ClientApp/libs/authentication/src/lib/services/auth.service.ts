import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap, shareReplay } from 'rxjs/operators';

import { AuthenticationConfig, AuthenticationResponse } from '../interfaces';
import { ModuleConfigToken } from '../token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<AuthenticationResponse>;

  constructor(@Inject(ModuleConfigToken) private authConfig: AuthenticationConfig,
              private http: HttpClient) {
    this.currentUserSubject
      = new BehaviorSubject<AuthenticationResponse>(
        JSON.parse(localStorage.getItem(this.authConfig.userStorageKey || 'currentUser'))
      );
  }

  public get currentUser$(): Observable<AuthenticationResponse> {
    return this.currentUserSubject.asObservable().pipe(shareReplay());
  }

  public get currentUser(): AuthenticationResponse {
    return this.currentUserSubject.value;
  }

  login(login: { username: string, password: string }): Observable<AuthenticationResponse> {
    return this.http.post<AuthenticationResponse>(this.authConfig.endpoint, login)
      .pipe(
        tap((response: AuthenticationResponse) => {
          if (response && response.token) {
            // this.localStorage.setItem('currentUser', response);
            localStorage.setItem(this.authConfig.userStorageKey || 'currentUser', JSON.stringify(response));
            this.currentUserSubject.next(response);
          }
        })
      );
  }

  logout(): void {
    // this.localStorage.removeItem('currentUser');
    localStorage.removeItem(this.authConfig.userStorageKey || 'currentUser');
    this.currentUserSubject.next(null);
  }
}
