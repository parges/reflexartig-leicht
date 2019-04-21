import { ReviewDialogComponent } from './review-dialog/review-dialog.component';
import { MAT_DATE_LOCALE, DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { catchError, map } from 'rxjs/operators';
import { ApiResponse } from '@rl/shared/models';
import { ActivatedRoute } from '@angular/router';
import { FormBase } from './../../utils/dynamic-forms/form-base';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { Customer } from 'src/app/customer/customer';
import { LoaderService } from './../../../../libs/shared/ui/services/loader.service';
import { MatDialog } from '@angular/material';
import { ApiService } from './../../../../libs/shared/api/src/lib/services/api.service';
import { Component, OnInit } from '@angular/core';
import { TestungFormControlService } from '../testung/testung-form-control.service';
import { SnackbarGenericComponent } from 'src/app/utils/snackbar-generic/snackbar-generic.component';
import { TestungChapter, Testung } from 'src/app/models/testung';
import { Review, ReviewChapter } from 'src/app/models/review';
import { of } from 'rxjs';
import { DebugRenderer2 } from '@angular/core/src/view/services';
import { MomentDateAdapter, MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss'],
  providers: [ TestungFormControlService,
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
export class ReviewComponent implements OnInit {

  id: number;
  private sub: any;
  questions : Map<ReviewChapter, FormBase<any>[]>  = new Map();
  reviewForm: FormGroup;
  payLoad = '';
  selectedPatient: Customer;
  currentReview: Review;

  testung: Testung = new Testung();

  form: FormGroup;

  private resource = `review`;
  private resourceDetailsPatient = `patient`;

  constructor(
    private $formService: TestungFormControlService,
    private api: ApiService,
    public dialog: MatDialog,
    private loader: LoaderService,
    private snackbar: SnackbarGenericComponent,
    private route: ActivatedRoute,
    private fb: FormBuilder) {

      this.reviewForm = this.fb.group({
        id : [''],
        fullname : ['', Validators.required],
        date : [''],
        observationsParents : [''],
        observationsChild : [''],
        exerciseAccomplishment : [''],
        problemHierarchies: this.fb.array([])
          // this.fb.group({
          //   Id: [''],
          //    initialValue: [''],
          //    changedValue: ['']
          // })
      });

  }

  get problems() {
    // var temp = this.reviewForm.get('problemHierarchies').controls as FormArray;
    // return temp;
    var temp = (<FormArray>this.reviewForm.get('problemHierarchies'))
    return temp.controls;
  }

  ngOnInit() {
    // this.openDialog();
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id']; // (+) converts string 'id' to a number
      this.api.getById<Review>(this.resource, this.id)
    .pipe(
      map((response: ApiResponse<Review>) => {
        this.loader.hideSpinner();
        return response.items;
      }),
      catchError(() => {
        this.loader.hideSpinner();
        return of([]);
      })
    )
    .subscribe((data: Review[]) => {
      this.currentReview = data[0];
      this.initFormWithData(data[0]);
      this.questions = this.$formService.getFormEntriesReview(this.currentReview.chapters);
      this.form = this.$formService.toFormGroupReview(this.questions);
    })
    });
  }

  initFormWithData(data: Review) {
    this.reviewForm.patchValue({
      id: data.id,
      fullname: data.name,
      date: data.date,
      observationsParents: data.observationsParents || '',
      observationsChild: data.observationsChild  || '',
      exerciseAccomplishment: data.exerciseAccomplishment  || '',
      // problemHierarchies: tmpArray
    });
    data.problemHierarchies.forEach(problem => {
      this.problems.push(this.fb.group({
        id: [problem.id],
        initialValue: [problem.initialValue],
        changedValue: [problem.changedValue]
      }));
    });
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(ReviewDialogComponent, {
      // width: '250px',
      data: {
        patientId: this.currentReview.patientId,
        review: this.currentReview
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(dialogRef.componentInstance.listOfChosenIds)
      {
        this.api.putBlankAction<Review>(this.resource, "UpdateReviewQuestions", this.currentReview.id, dialogRef.componentInstance.listOfChosenIds)
        .pipe(
          map((data: Review) => {
            this.loader.hideSpinner();
            return data;
          })
        ).subscribe((data: Review) => {
          this.currentReview = data
          this.initFormWithData(data);
          this.questions = this.$formService.getFormEntriesReview(this.currentReview.chapters);
          this.form = this.$formService.toFormGroupReview(this.questions);
        });
      }
      });
  }

  onSubmit() {
    var tempData: any = Object.assign({}, this.reviewForm.getRawValue());
    var review: Review = this.currentReview;
    review.name = tempData.fullname;
    review.date = tempData.date;
    review.observationsChild = tempData.observationsChild;
    review.observationsParents = tempData.observationsParents;
    review.exerciseAccomplishment = tempData.exerciseAccomplishment;
    review.problemHierarchies = tempData.problemHierarchies;

    const result: Customer = Object.assign({}, this.form.value);
      var index = 1;
      this.currentReview.chapters.forEach(chapter => {
        chapter.questions.forEach(question => {
          var _chapter = chapter;
          var group = this.form.get(chapter.id.toString());
          question.value = this.form.get(chapter.id.toString()).value["question_" + question.id];
          index++;
        })
      });

    this.api.putBlankAction<Review>(this.resource, "UpdateReview", this.currentReview.id, review)
    .pipe(
      map((data: Review) => {
        this.loader.hideSpinner();
        this.currentReview = data;
        return data;
      })
    ).subscribe((data: Review) => {
      this.snackbar.openSnackBar('Gespeichert');
    });


  }

  deleteSelectedTests() {
    this.api.deleteAction<Review>(this.resource, "DeleteTests", this.currentReview.id)
    .pipe(
      map(() => {
        this.loader.hideSpinner();
      }))
    .subscribe(() => {
      this.snackbar.openSnackBar('Gelöscht');
      // Review erneut lesen
      this.api.getById<Review>(this.resource, this.currentReview.id)
      .pipe(
        map((response: ApiResponse<Review>) => {
          this.loader.hideSpinner();
          return response.items;
        }),
        catchError(() => {
          this.loader.hideSpinner();
          return of([]);
        })
      )
      .subscribe((data: Review[]) => {
        this.currentReview = data[0];
        this.initFormWithData(data[0]);
        this.questions = this.$formService.getFormEntriesReview(this.currentReview.chapters);
        this.form = this.$formService.toFormGroupReview(this.questions);
      })
    });
  }

  descOrder = (a, b) => {
    return null;
  }

  // radioCheckUncheck( currentValue: any, chapter: TestungChapter, formKey: string) {
  //   var newValue = this.form.get(chapter.id.toString()).value[formKey];
  //   if (newValue === currentValue) {
  //     this.form.get(chapter.id.toString()).get(formKey).reset();
  //   }
  // }
}
