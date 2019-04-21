import { LoaderService } from './../../../libs/shared/ui/services/loader.service';
import { AuthService } from './../../../libs/authentication/src/lib/services/auth.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'dvz-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  loginForm: FormGroup;
  returnUrl: string;
  error = '';

  private loginSub: Subscription;

  constructor(private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private auth: AuthService,
              private loader: LoaderService) {
    // redirect to home if already logged in
    if (this.auth.currentUser) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  onSubmit(): void {
    this.loader.showSpinner();

    this.loginSub = this.auth.login(this.loginForm.value)
      .subscribe(
        () => {
          this.loader.hideSpinner();
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.error = error;
          this.loader.hideSpinner();
        });
  }

  ngOnDestroy(): void {
    if (this.loginSub && !this.loginSub.closed) {
      this.loginSub.unsubscribe();
    }
  }
}
