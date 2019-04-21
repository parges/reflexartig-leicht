import { SnackbarGenericComponent } from './../../utils/snackbar-generic/snackbar-generic.component';
import { ApiResponse } from './../../../../libs/shared/models/src/lib/interfaces/interfaces.common';
import { map, catchError, switchMap } from 'rxjs/operators';
import { LoaderService } from './../../../../libs/shared/ui/services/loader.service';
import { ApiService } from './../../../../libs/shared/api/src/lib/services/api.service';
import { FormGroup, FormBuilder, Validators, NgForm, FormArray } from '@angular/forms';
import { Component, OnInit, OnDestroy, AfterViewInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer } from 'src/app/customer/customer';

import { MAT_DATE_LOCALE} from '@angular/material/core';
import { BreakpointObserver } from '@angular/cdk/layout';
import { of, timer } from 'rxjs';

@Component({
  selector: 'app-cust-get',
  templateUrl: './cust-get.component.html',
  styleUrls: ['./cust-get.component.scss'],
  providers: [
    // The locale would typically be provided on the root module of your application. We do it at
    // the component level here, due to limitations of our example generation script.
    {provide: MAT_DATE_LOCALE, useValue: 'de-DE'},

  ],
})
export class CustGetComponent implements OnInit, OnDestroy {
  id: number;
  private sub: any;
  activeCustomer: Customer;
  tempCustomer: Customer;

  regiForm: FormGroup;
  formBuilder: FormBuilder;
  selectedFile: File;

  imageToShow: any;
  isImageLoading: boolean;

  private resource = `patient`;

  @ViewChild("uploader") uploader: any;

  constructor(
    private route: ActivatedRoute,
    private $router: Router,
    private fb: FormBuilder,
    private api: ApiService,
    private loader: LoaderService,
    public snackbar: SnackbarGenericComponent) {
    this.formBuilder = fb;
    this.regiForm = this.formBuilder.group({
      id : [''],
      firstname : ['', Validators.required],
      lastname : ['', Validators.required],
      tele : ['', Validators.required],
      birthday : [''],
      age : [''],
      avatar : [''],
      address : [''],
      reviews : this.fb.array([])
    });
   }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id']; // (+) converts string 'id' to a number
      this.api.getById<Customer>(this.resource, this.id)
    .pipe(
      map((response: ApiResponse<Customer>) => {
        this.loader.hideSpinner();
        return response.items;
      }),
      catchError(() => {
        this.loader.hideSpinner();
        return of([]);
      })
    )
    .subscribe((data: Customer[]) => {
      this.activeCustomer = data[0];
      // Calculate the age
      const timeDiff = Math.abs(Date.now() - new Date(this.activeCustomer.birthday).getTime());
      const age = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25);
      // To initialize FormGroup
      this.regiForm.setValue({
        id: this.activeCustomer.id,
        firstname: this.activeCustomer.firstname,
        lastname: this.activeCustomer.lastname,
        tele: this.activeCustomer.tele,
        birthday: this.activeCustomer.birthday,
        age: age,
        avatar: this.activeCustomer.avatar,
        address: 'Max Mustermann Straße',
        reviews: this.fillReviews()
      });
    });

    //   this.$customer.getCustomerByID(this.id)
    //   .then(cust => {
    //     this.activeCustomer = cust;
    //     // Calculate the age
    //     const timeDiff = Math.abs(Date.now() - new Date(this.activeCustomer.birthday).getTime());
    //     const age = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25);
    //     // To initialize FormGroup
    //     this.regiForm.setValue({
    //       id: this.activeCustomer.id,
    //       firstname: this.activeCustomer.firstname,
    //       lastname: this.activeCustomer.lastname,
    //       tele: this.activeCustomer.tele,
    //       birthday: this.activeCustomer.birthday,
    //       age: age,
    //       avatar: this.activeCustomer.avatar,
    //       address: 'Max Mustermann Straße',
    //       reviews: this.fillReviews()
    //     });
    //   })
    //   .then(cust => {
    //     this.isImageLoading = true;

    //     this.$customer.getImageByFilename(this.activeCustomer.id).subscribe(data => {
    //       this.createImageFromBlob(data);
    //       this.isImageLoading = false;
    //     }, error => {
    //       this.isImageLoading = false;
    //       console.log(error);
    //     });
    //   });
     });
  }

  fillReviews(): FormGroup {
    let reviews = this.regiForm.get('reviews') as FormArray;
    if(this.activeCustomer.reviews.length > 0) {
      this.activeCustomer.reviews.forEach(review => {
        reviews.push(
        this.fb.group({
          name: review.name,
          date: review.date,
          payed: review.payed,
          exercises: review.exercises,
          reasons: review.reasons
        })
        );
      });
    }
    // initialize our address
    return this.fb.group({
        name: ['Anfangsübung'],
        date: [''],
        payed: [''],
        exercises: ['Übungen'],
        reasons: ['BEgründungen']
    });
  }

  createImageFromBlob(image: Blob) {
     let reader = new FileReader();
     reader.addEventListener("load", () => {
        this.imageToShow = reader.result;
     }, false);

     if (image) {
        reader.readAsDataURL(image);
     }
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  fileChange(files: FileList) {
      if (files && files[0].size > 0) {
        this.regiForm.patchValue({
          avatar: files[0]
        });
      }
  }

  onSubmit() {
    const result: Customer = Object.assign({}, this.regiForm.value);
    this.api.put<Customer>(this.resource, this.activeCustomer.id, result)
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
    .subscribe(() => {
      this.snackbar.openSnackBar("Gespeichert");
      // const source = timer(1);
      // const subscribe = source.subscribe(() => {
      this.$router.navigate(['customers']);
      // });
    });
  }

  // Executed When Form Is Submitted
  onDeleteCustomer() {
    // Make sure to create a deep copy of the form-model
    const result: Customer = Object.assign({}, this.regiForm.value);
    this.api.delete(this.resource, result.id).subscribe((data: Customer[]) => {
      this.$router.navigate(['customers']);
    });
  }
}
