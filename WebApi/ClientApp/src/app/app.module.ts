import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './root/app.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthErrorInterceptor } from './../../libs/authentication/src/lib/interceptors/auth-error.interceptor';
import { AuthInterceptor } from './../../libs/authentication/src/lib/interceptors/auth.interceptor';
import { MaterialModule } from './../../libs/material/src/lib/material.module';
import { ApiModule } from './../../libs/shared/api/src/lib/api.module';
import { AuthenticationModule } from './../../libs/authentication/src/lib/authentication.module';
import { DocumentsModule } from './documents/documents.module';
import { NoCachInterceptorService } from './interceptor/httpconfig.interceptor';
import { CustomerEditModule } from './customer-edit/customer-edit.module';
import { CustomerOverviewModule } from './customer-overview/customer-overview.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

// tslint:disable-next-line:max-line-length
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SnackbarGenericComponent } from './utils/snackbar-generic/snackbar-generic.component';
import { environment } from 'src/environments/environment.dev';


@NgModule({
  declarations: [
    AppComponent,
    SnackbarGenericComponent,
    LoginComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MaterialModule,
    CustomerOverviewModule,
    CustomerEditModule,
    DocumentsModule,
    AuthenticationModule.forRoot({
      endpoint: `${environment.api.host}:${environment.api.port}/${environment.api.suffix}/Token`
    }),
    ApiModule.forRoot({
      endpoint: `${environment.api.host}:${environment.api.port}/${environment.api.suffix}`
    }),
  ],
  // providers   : [
  // {
  //   provide : HTTP_INTERCEPTORS,
  //   useClass: NoCachInterceptorService, multi: true

  // },
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
