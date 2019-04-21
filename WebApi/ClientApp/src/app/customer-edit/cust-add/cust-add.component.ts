import { LoaderService } from './../../../../libs/shared/ui/services/loader.service';
import { ApiResponse } from './../../../../libs/shared/models/src/lib/interfaces/interfaces.common';
import { ApiService } from './../../../../libs/shared/api/src/lib/services/api.service';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer } from 'src/app/customer/customer';

import { MAT_DATE_LOCALE} from '@angular/material/core';
import { switchMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';


@Component({
  selector: 'app-cust-add',
  templateUrl: './cust-add.component.html',
  styleUrls: ['./cust-add.component.scss'],
  providers: [
    // The locale would typically be provided on the root module of your application. We do it at
    // the component level here, due to limitations of our example generation script.
    {provide: MAT_DATE_LOCALE, useValue: 'de-DE'},

  ],
})
export class CustAddComponent {

  addCustForm: FormGroup;
  private resource = `patient`;

  constructor(private $router: Router, private fb: FormBuilder, private api: ApiService, private loader: LoaderService) {
    this.addCustForm = this.fb.group({
      'id' : [''],
      'firstname' : ['', Validators.required],
      'lastname' : ['', Validators.required],
      'tele' : ['', Validators.required],
      'birthday' : ['']
    });
   }

  onFormSubmit() {
    const result: Customer = Object.assign({}, this.addCustForm.value);
    this.api.post<Customer>(this.resource, result)
    .pipe(
      switchMap(() => {
        return this.api.get<Customer>(this.resource);
      }),
      map((data: ApiResponse<Customer>) => {
        this.loader.hideSpinner();
        return data.items;
      }),
      catchError(() => {
        this.loader.hideSpinner();
        return of([]);
      })
    )
    .subscribe((data: Customer[]) => {
      // this.$router.navigate(['customers']);
    });
  }
}
