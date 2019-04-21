import { MaterialModule } from './../../../libs/material/src/lib/material.module';
import { CustomerModule } from './../customer/customer.module';
import { MatTableModule, MatPaginatorModule, MatSortModule, MatProgressSpinnerModule } from '@angular/material';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustListComponent } from './cust-list/cust-list.component';

@NgModule({
  declarations: [CustListComponent],
  imports: [
    CommonModule,
    CustomerModule,
    MaterialModule
  ],
  exports: [
    CustListComponent,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule
  ]
})
export class CustomerOverviewModule { }
