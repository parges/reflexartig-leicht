import { NgModule } from '@angular/core';
import {
  MatToolbarModule,
  MatGridListModule,
  MatIconModule,
  MatButtonModule,
  MatPaginatorModule,
  MatTableModule,
  MatDialogModule,
  MatProgressSpinnerModule,
  MatInputModule,
  MatSortModule,
  MatCheckboxModule,
  MatSpinner,
  MatDatepickerModule,
  MatSelectModule,
  MatStepperModule,
  MAT_DATE_LOCALE,
  MatDividerModule,
  MatAutocompleteModule,
  MatCardModule,
  MatSnackBarModule
} from '@angular/material';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { LayoutModule } from '@angular/cdk/layout';
import { OverlayModule } from '@angular/cdk/overlay';

const materialModules: any[] = [
  MatButtonModule,
  MatCheckboxModule,
  MatDatepickerModule,
  MatMomentDateModule,
  MatDialogModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatDialogModule,
  MatPaginatorModule,
  MatProgressSpinnerModule,
  MatSelectModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatToolbarModule,
  MatDividerModule,
  MatAutocompleteModule,
  MatCardModule,
  MatSnackBarModule
];

const cdkModules: any[] = [
  LayoutModule,
  OverlayModule
];

@NgModule({
  imports: [cdkModules, materialModules],
  exports: [cdkModules, materialModules],
  entryComponents: [MatSpinner],
  providers: [
    // setting DatePicker locale
    {
      provide: MAT_DATE_LOCALE,
      useValue: 'de-DE'
    }
  ]
})
export class MaterialModule { }
