import { AnamneseFormControlService } from './anamnese-form-control.service';
import { AnamneseChapter, Anamnese } from './../../models/anamnese';
import { Component, OnInit } from '@angular/core';
import { FormBase } from 'src/app/utils/dynamic-forms/form-base';
import { FormGroup } from '@angular/forms';
import { Customer } from 'src/app/customer/customer';
import { ApiService } from '@rl/shared/api';
import { MatDialog } from '@angular/material';
import { LoaderService } from 'libs/shared/ui/services';
import { SnackbarGenericComponent } from 'src/app/utils/snackbar-generic/snackbar-generic.component';
import { PatientAutocompleteDialog } from 'src/app/utils/patient-external-dialog/patient-external-dialog.component';
import { map } from 'rxjs/operators';
import { ApiResponse } from '@rl/shared/models';

@Component({
  selector: 'app-anamnese01',
  templateUrl: './anamnese01.component.html',
  styleUrls: ['./anamnese01.component.scss'],
  providers: [ AnamneseFormControlService ]
})
export class Anamnese01Component implements OnInit {

  questions : Map<AnamneseChapter, FormBase<any>[]>  = new Map();
  form: FormGroup;
  payLoad = '';
  selectedPatient: Customer;

  anamnese: Anamnese = new Anamnese();

  meta = '_meta';

  private resource = `anamnese`;

  constructor(private $formService: AnamneseFormControlService, private api: ApiService, public dialog: MatDialog, private loader: LoaderService,
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
        this.api.getById<Anamnese>(this.resource, this.selectedPatient.id)
        .pipe(
          map((data: ApiResponse<Anamnese>) => {
            this.loader.hideSpinner();
            this.anamnese = data.items[0];
          })
        ).subscribe(() => {
          this.questions = this.$formService.getFormEntries(this.anamnese);
          this.form = this.$formService.toFormGroup(this.questions);
        });
      }
      });
  }

  onSubmit() {
    // this.payLoad = JSON.stringify(this.form.value);
    const result: Customer = Object.assign({}, this.form.value);
    var index = 1;
    this.anamnese.chapters.forEach(chapter => {
      chapter.questions.forEach(question => {
        var _chapter = chapter;
        var group = this.form.get(chapter.id.toString());
        question.value = this.form.get(chapter.id.toString()).value["question_" + index];
        var meta = this.form.get(chapter.id.toString()).value["question_" + index+"_meta"] || '';
        if(meta.length > 0){
          debugger;
          question.textValue = meta;
        }
        index++;
      })
    });
    debugger;
    this.api.put<Anamnese>(this.resource, this.selectedPatient.id, this.anamnese)
    .pipe(
      map((data: ApiResponse<Anamnese>) => {
        this.loader.hideSpinner();
        this.anamnese = data[0];
        return data[0];
      })
    ).subscribe((anamnese: Anamnese) => {
      this.snackbar.openSnackBar('Gespeichert');
      this.questions = this.$formService.getFormEntries(anamnese);
      this.form = this.$formService.toFormGroup(this.questions);
    });
  }

  descOrder = (a, b) => {
    return null;
  }

  showSnackbarMetaInfos(meta: string) {
    this.snackbar.openSnackBar(meta);
  }
}
