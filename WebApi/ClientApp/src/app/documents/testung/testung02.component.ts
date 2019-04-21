import { TestungChapter } from './../../models/testung';
import { SnackbarGenericComponent } from './../../utils/snackbar-generic/snackbar-generic.component';
import { LoaderService } from './../../../../libs/shared/ui/services/loader.service';
import { map } from 'rxjs/operators';
import { ApiResponse } from './../../../../libs/shared/models/src/lib/interfaces/interfaces.common';
import { ApiService } from './../../../../libs/shared/api/src/lib/services/api.service';
import { PatientAutocompleteDialog } from './../../utils/patient-external-dialog/patient-external-dialog.component';
import { MatDialog } from '@angular/material';
import { Customer } from './../../customer/customer';
import { TestungFormControlService } from './testung-form-control.service';
import { FormGroup } from '@angular/forms';
import { FormBase } from './../../utils/dynamic-forms/form-base';
import { Component, OnInit, Input } from '@angular/core';
import { Testung } from 'src/app/models/testung';
import { debugOutputAstAsTypeScript } from '@angular/compiler';

@Component({
  selector: 'app-testung02',
  templateUrl: './testung02.component.html',
  styleUrls: ['./testung02.component.scss'],
  providers: [ TestungFormControlService ]

})
export class Testung02Component implements OnInit {

  questions : Map<TestungChapter, FormBase<any>[]>  = new Map();
  form: FormGroup;
  payLoad = '';
  selectedPatient: Customer;

  testung: Testung = new Testung();

  private resource = `testung`;

  constructor(private $formService: TestungFormControlService, private api: ApiService, public dialog: MatDialog, private loader: LoaderService,
    private snackbar: SnackbarGenericComponent) {
  }

  ngOnInit() {
    this.openDialog();
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
        this.selectedPatient = dialogRef.componentInstance.selectedPatient;
        this.api.getById<Testung>(this.resource, this.selectedPatient.id)
        .pipe(
          map((data: ApiResponse<Testung>) => {
            this.loader.hideSpinner();
            this.testung = data.items[0];
          })
        ).subscribe(() => {
          this.questions = this.$formService.getFormEntries(this.testung);
          this.form = this.$formService.toFormGroup(this.questions);
        });
      }
      });
  }

  onSubmit() {
    // this.payLoad = JSON.stringify(this.form.value);
    const result: Customer = Object.assign({}, this.form.value);
    var index = 1;
    this.testung.chapters.forEach(chapter => {
      chapter.questions.forEach(question => {
        var _chapter = chapter;
        var group = this.form.get(chapter.id.toString());
        question.value = this.form.get(chapter.id.toString()).value["question_" + index];
        index++;
      })
    });
    this.api.put<Testung>(this.resource, this.selectedPatient.id, this.testung)
    .pipe(
      map((data: ApiResponse<Testung>) => {
        this.loader.hideSpinner();
        this.testung = data[0];
        return data[0];
      })
    ).subscribe((testung: Testung) => {
      this.snackbar.openSnackBar('Gespeichert');
      this.questions = this.$formService.getFormEntries(testung);
      this.form = this.$formService.toFormGroup(this.questions);
    });
  }

  descOrder = (a, b) => {
    return null;
  }

  radioCheckUncheck( currentValue: any, chapter: TestungChapter, formKey: string) {
    var newValue = this.form.get(chapter.id.toString()).value[formKey];
    if (newValue === currentValue) {
      this.form.get(chapter.id.toString()).get(formKey).reset();
    }
  }

}
