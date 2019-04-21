import { MomentDateAdapter, MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';
import { MAT_DATE_LOCALE, DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { ApiResponse } from '@rl/shared/models';
import { Router } from '@angular/router';
import { ApiService } from './../../../../libs/shared/api/src/lib/services/api.service';
import { PatientAutocompleteDialog } from './../../utils/patient-external-dialog/patient-external-dialog.component';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import { Component, AfterViewInit, Inject, OnInit, ViewChild, ElementRef } from '@angular/core';
import { UebersichtModel } from './ubersichtModel';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Customer } from 'src/app/customer/customer';
import { SnackbarGenericComponent } from 'src/app/utils/snackbar-generic/snackbar-generic.component';
import { Review } from 'src/app/models/review';

export interface DialogData {
  patient: Customer;
}

@Component({
  selector: 'app-uebersicht00',
  templateUrl: './uebersicht00.component.html',
  styleUrls: ['./uebersicht00.component.scss'],
  providers: [
    // The locale would typically be provided on the root module of your application. We do it at
    // the component level here, due to limitations of our example generation script.
    {provide: MAT_DATE_LOCALE, useValue: 'de-DE'},

    // `MomentDateAdapter` and `MAT_MOMENT_DATE_FORMATS` can be automatically provided by importing
    // `MatMomentDateModule` in your applications root module. We provide it at the component level
    // here, due to limitations of our example generation script.
    {provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
    {provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS},
  ]
})
export class Uebersicht00Component implements AfterViewInit{


  docUebersicht: FormGroup;
  formModel: UebersichtModel;
  selectedPatient: Customer;
  patients: Customer[];
  activeCustomer: Customer;

  isDisabled: boolean = true;
  disabledColumns: string[] = ['firstname','lastname', 'tele', 'address', 'birthday']

  private resource = `patient`;
  private resourceReview = `review`;

  constructor(private api: ApiService, private fb: FormBuilder, public dialog: MatDialog, public snackbar: SnackbarGenericComponent, private router: Router) {
    this.docUebersicht = this.fb.group({
      id : [''],
      firstname : ['', Validators.required],
      lastname : ['', Validators.required],
      tele : ['', Validators.required],
      birthday : [''],
      address : [''],
      anamneseDate: [''],
      anamnesePayed: [''],
      diagnostikDate: [''],
      diagnostikPayed: [''],
      elternDate: [''],
      elternPayed: [''],
      problemHierarchy: [''],
      reviews : this.fb.array([])
    });

    this.enableControlStates(false);

    this.openDialog();
   }

   ngAfterViewInit() {

   }

   enableControlStates(enable: boolean) {
    if(enable){
      Object.keys(this.docUebersicht.controls).forEach(key => {
        if(this.disabledColumns.indexOf(key) != -1){
          this.docUebersicht.get(key).enable();
        }
      });
    } else {

      Object.keys(this.docUebersicht.controls).forEach(key => {
        if(this.disabledColumns.indexOf(key) != -1){
          this.docUebersicht.get(key).disable();
        }
      });
    }

   }

   openDialog(): void {
    const dialogRef = this.dialog.open(PatientAutocompleteDialog, {
      width: '250px',
      data: {patient: this.selectedPatient}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      if(dialogRef.componentInstance.selectedPatient)
      {
        this.isDisabled = false;

        this.activeCustomer = dialogRef.componentInstance.selectedPatient;
        this.initFormGroupWithData();
      }
    });
  }
  initFormGroupWithData() {
    // To initialize FormGroup
    this.docUebersicht.setValue({
      id: this.activeCustomer.id,
      firstname: this.activeCustomer.firstname,
      lastname: this.activeCustomer.lastname,
      tele: this.activeCustomer.tele,
      birthday: this.activeCustomer.birthday ? this.activeCustomer.birthday : '',
      address: this.activeCustomer.address,
      anamneseDate: this.activeCustomer.anamneseDate,
      anamnesePayed: this.activeCustomer.anamnesePayed,
      diagnostikDate: this.activeCustomer.diagnostikDate,
      diagnostikPayed: this.activeCustomer.diagnostikPayed,
      elternDate: this.activeCustomer.elternDate,
      elternPayed: this.activeCustomer.elternPayed,
      problemHierarchy: this.activeCustomer.problemHierarchy,
      reviews: this.fillReviews()
      // reviews: this.fb.array([
      //   this.initReviews(),
      //   this.initReviews()
      // ])
      });
  }
  fillReviews(): FormGroup {
    let reviews = this.docUebersicht.get('reviews') as FormArray;
    if(this.activeCustomer.reviews.length > 0){
      this.activeCustomer.reviews.forEach(review => {
        reviews.push(
        this.fb.group({
          id: review.id,
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
        id: [''],
        name: ['Anfangsübung'],
        date: [''],
        payed: [''],
        exercises: ['Übungen'],
        reasons: ['BEgründungen']
    });
  }
  initReviews(review?: Review) {
    if(!review){
      // initialize our address
      return this.fb.group({
          id: [''],  //todo
          name: ['neuer Eintrag'],
          date: [''],
          payed: [false],
          exercises: [''],
          reasons: ['']
      });
    } else {
      return this.fb.group({
        id: [review.id],
        name: [review.name],
        date: [review.date],
        payed: [review.payed],
        exercises: [review.exercises],
        reasons: [review.reasons],
    });
    }
  }
  addReview() {
    this.api.postBlank<Review>(this.resourceReview, this.activeCustomer)
    .subscribe((review: Review) =>
    {
      const control = <FormArray>this.docUebersicht.controls['reviews'];
      control.push(this.initReviews(review));
    });
  }
  removeReview(review: FormGroup, i : number) {

    var id = review.controls.id.value;
    this.api.delete<Review>(this.resourceReview, id)
    .subscribe(() =>
    {
      this.api.getById<Customer>(this.resource, this.activeCustomer.id)
      .subscribe((data: ApiResponse<Customer>) => {
        this.activeCustomer = data.items[0];
        const control = <FormArray>this.docUebersicht.controls['reviews'];
        control.removeAt(i);
      }
      );
      // const control = <FormArray>this.docUebersicht.controls['reviews'];
      // control.push(this.initReviews(review[0]));
    });
  }

  updateReview(review: FormGroup) {
    this.router.navigate(['reviews/', review.controls.id.value]);
  }

  onDocSubmit() {
    this.enableControlStates(true);
    const result: Customer = Object.assign({}, this.docUebersicht.value);
    this.api.put<Customer>(this.resource, result.id, result)
    .subscribe(() => {
      this.snackbar.openSnackBar('Gespeichert');
      this.enableControlStates(false);
    })
    // this.$customer.updateCustomer(result).catch(
    //   err => console.error(err)
    // ).finally(() => {

    //   // this.$router.navigate(['customers']);
    // });

  }


}

