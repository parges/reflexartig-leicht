import { AuthService } from './../../../libs/authentication/src/lib/services/auth.service';
import { AuthenticationResponse } from './../../../libs/authentication/src/lib/interfaces/index';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public currentUser$: Observable<AuthenticationResponse> = this.auth.currentUser$;

  constructor(private auth: AuthService) {}

  logout(): void {
    this.auth.logout();
    location.reload();
  }
}
