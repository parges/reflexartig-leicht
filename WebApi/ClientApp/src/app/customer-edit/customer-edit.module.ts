import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustAddComponent } from './cust-add/cust-add.component';
import { CustGetComponent } from './cust-get/cust-get.component';
// tslint:disable-next-line:max-line-length
import { MatInputModule, MatFormFieldModule, MatOptionModule, MatSelectModule, MatButtonModule, MatDatepickerModule, MatNativeDateModule } from '@angular/material';

import { FormBuilder, FormGroup, Validators , FormsModule, NgForm } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { UploadComponent } from '../utils/upload/upload.component';


@NgModule({
  declarations: [CustAddComponent, CustGetComponent, UploadComponent ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  exports: [CustAddComponent,
    CustGetComponent,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class CustomerEditModule { }
