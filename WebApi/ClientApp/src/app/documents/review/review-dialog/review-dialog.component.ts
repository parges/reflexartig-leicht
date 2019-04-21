import { FormBase } from 'src/app/utils/dynamic-forms/form-base';
import { LoaderService } from './../../../../../libs/shared/ui/services/loader.service';
import { ApiService } from './../../../../../libs/shared/api/src/lib/services/api.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable, merge } from 'rxjs';
import { Customer } from 'src/app/customer/customer';
import { DialogData } from '../../uebersicht/uebersicht00.component';
import { startWith, switchMap, map } from 'rxjs/operators';
import { isString, isObject } from 'util';
import { TestungChapter, Testung } from 'src/app/models/testung';
import { TestungFormControlService } from '../../testung/testung-form-control.service';
import { ApiResponse } from 'libs/shared/models/src/lib/interfaces';
import { ReviewQuestion, Review } from 'src/app/models/review';

export interface DialogDataReview {
  patientId: number;
  review: Review;
}

@Component({
  selector: 'app-review-dialog',
  templateUrl: './review-dialog.component.html',
  styleUrls: ['./review-dialog.component.scss'],
  providers: [ TestungFormControlService ]
})
export class ReviewDialogComponent implements OnInit {

  questions : Map<TestungChapter, FormBase<any>[]>  = new Map();
  testung: Testung = new Testung();
  form: FormGroup;
  listOfChosenIds: number[] = [];
  private resource = `testung`;

  constructor(private api: ApiService,
    public dialogRef: MatDialogRef<ReviewDialogComponent>, private loader: LoaderService,
    private $formService: TestungFormControlService,
    @Inject(MAT_DIALOG_DATA) public data: DialogDataReview) {
      this.api.getById<Testung>(this.resource, data.patientId)
        .pipe(
          map((returnData: ApiResponse<Testung>) => {
            this.loader.hideSpinner();
            debugger;
            returnData.items[0].chapters = returnData.items[0].chapters.sort((a, b) => b.score - a.score);
            this.testung = returnData.items[0];
          })
        ).subscribe(() => {
          this.setCheckedQuestions(data.review);
          this.questions = this.$formService.getFormEntries(this.testung);
          this.form = this.$formService.toFormGroup(this.questions);
        });
      // merge()
      //   .pipe(
      //     startWith({}),
      //   switchMap(() => {
      //     return this.api.get<Customer>(this.resource);
      //   })
      //   ).subscribe(resp => {
      //     this.patients = resp.items;
      //     this.filteredPatients = this.myControl.valueChanges
      //     .pipe(
      //       startWith(''),
      //       map(patient => patient ? this._filterPatients(patient) : this.patients.slice())
      //     );
      //   });
    }

  ngOnInit() {
  }
  onClose(): void {
    this.dialogRef.close();
  }

  questionAdd(question: FormBase<any>) {
    if (this.listOfChosenIds.indexOf(question.id) == -1) {
      this.listOfChosenIds.push(question.id);
    } else {
      const index = this.listOfChosenIds.indexOf(question.id, 0);
      if (index > -1) {
        this.listOfChosenIds.splice(index, 1);
      }
    }
  }

  checkChosen(id: number) {
    return (this.listOfChosenIds.indexOf(id) >= 0) ? true : false;
  }

  setCheckedQuestions(review: Review): void {
    review.chapters.forEach(chapter => {
      chapter.questions.forEach(question => {
        this.testung.chapters.forEach(chapter => {
          var foundItem = chapter.questions.find(q => q.label == question.label);
          if (foundItem != null) {
            this.listOfChosenIds.push(foundItem.id);
          }
          // } else {
          //   bFound = true;
          // }
        })
      });
    });


  }

  descOrder = (a, b) => {
    return null;
  }
}
