import { ChartDialogComponent } from './../../utils/chart-dialog/chart-dialog.component';
import { HttpHeaders } from '@angular/common/http';
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
import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { Testung } from 'src/app/models/testung';
import * as jsPDF from 'jspdf';

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

  possibleColors: string[] = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)'];



  @ViewChild('printContent') printContent: ElementRef;

  private resource = `testung`;
  private resoureDoc = `document`;

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
  showBarChart() {
    var _labels: string[] = [];
    var _data: number[] = [];
    var _bgColor: string[] = []
    var _borderColor: string[] = []
    this.testung.chapters.forEach(c => {
      if(c.score > -1) {
        _bgColor.push(this.possibleColors[_labels.length]);
        _labels.push(c.name.slice(0, c.name.indexOf(".")));
        _data.push(c.score);
      }
    });

    const dialogRef = this.dialog.open(ChartDialogComponent, {
      height: 'auto',
      width: '80%',
      data: {
        title: 'Testung Chart',
        datasetLabel: 'Bewertung',
        labels: _labels,
        rows: _data,
        bgColor: _bgColor,
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

  downloadPDF() {
    // let doc = new jsPDF();

    // let specialElementHandlers = {
    //   '#editor': function(element, renderer) {
    //     return true;
    //   }
    // };

    // let content = this.printContent.nativeElement;

    // doc.fromHTML(content.innerHTML, 15, 15, {
    //   'width': 190,
    //   'elementHandlers': specialElementHandlers
    // });

    // doc.save('Testung_' + this.selectedPatient.lastname + '.pdf');

    return this.api.downloadDocumentType(this.resoureDoc, 2);

  }

}
