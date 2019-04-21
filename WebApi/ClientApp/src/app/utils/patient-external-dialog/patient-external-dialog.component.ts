import { LoaderService } from './../../../../libs/shared/ui/services/loader.service';
import { ApiResponse } from './../../../../libs/shared/models/src/lib/interfaces/interfaces.common';
import { ApiService } from './../../../../libs/shared/api/src/lib/services/api.service';
import { isString, isObject } from 'util';
import { DialogData } from './../../documents/uebersicht/uebersicht00.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Customer } from './../../customer/customer';
import { Observable, merge, of } from 'rxjs';
import { FormControl } from '@angular/forms';
import { Component, OnInit, Inject } from '@angular/core';
import { startWith, switchMap, map, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-patient-external-dialog',
  templateUrl: './patient-external-dialog.component.html',
  styleUrls: ['./patient-external-dialog.component.scss']
})

export class PatientAutocompleteDialog {

  myControl = new FormControl();
  filteredPatients: Observable<Customer[]>;
  patients: Customer[];
  selectedPatientId: number;

  selectedPatient: Customer;
  private resource = `patient`;

  constructor(private api: ApiService,
    public dialogRef: MatDialogRef<PatientAutocompleteDialog>, private loader: LoaderService,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {

      merge()
        .pipe(
          startWith({}),
        switchMap(() => {
          return this.api.get<Customer>(this.resource);
        })
        ).subscribe(resp => {
          this.patients = resp.items;
          this.filteredPatients = this.myControl.valueChanges
          .pipe(
            startWith(''),
            map(patient => patient ? this._filterPatients(patient) : this.patients.slice())
          );
        });
    }

    public _filterPatients(value: any): Customer[] {
      if (isString(value)) {
        const filteredValue = value.toLowerCase();
        return this.patients.filter(patient => patient.firstname.toLowerCase().indexOf(filteredValue) === 0 || patient.lastname.toLowerCase().indexOf(filteredValue)  === 0 );
      } else if(isObject(value)){
        const filterValueFirstname = value.firstname.toLowerCase();
        const filterValueLastname = value.lastname.toLowerCase();
        return this.patients.filter(patient => patient.firstname.toLowerCase().indexOf(filterValueFirstname) === 0 || patient.lastname.toLowerCase().indexOf(filterValueLastname)  === 0 );
      }
      else {
        return this.patients;
      }
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  public getDisplayFn() {
    return (val) => this.display(val);
  }
  public display(item: Customer): string {
      if (item) {
        return item.firstname + ' ' + item.lastname;
      }
  }
  selectionChange(customer) {
    this.selectedPatient = customer;
    this.dialogRef.close();
   }

}
