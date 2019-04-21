import { TestungChildrenComponent } from './testungChildren/testungChildren.component';
import { MaterialModule } from './../../../libs/material/src/lib/material.module';
import { PatientAutocompleteDialog } from './../utils/patient-external-dialog/patient-external-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Uebersicht00Component } from './uebersicht/uebersicht00.component';
import { Testung02Component } from './testung/testung02.component';
import { Anamnese01Component } from './anamnese/anamnese01.component';
import {MatTableModule, MatSortModule, MatExpansionModule, MatTabsModule, MatGridListModule, MatListModule} from '@angular/material';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OverviewTableComponent } from './overview-table/overview-table.component';
import { ReviewComponent } from './review/review.component';
import { ReviewDialogComponent } from './review/review-dialog/review-dialog.component';

@NgModule({
  // tslint:disable-next-line:max-line-length
  declarations: [OverviewTableComponent, Anamnese01Component, Testung02Component, Uebersicht00Component, PatientAutocompleteDialog, TestungChildrenComponent, ReviewComponent, ReviewDialogComponent ],
  imports: [
    CommonModule,
    MatTableModule,
    MatSortModule,
    MatExpansionModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    MatTabsModule,
    MatGridListModule,
    MatListModule
  ],
  exports: [
  ],
  entryComponents: [PatientAutocompleteDialog, ReviewDialogComponent],

})
export class DocumentsModule { }
