import { TestungChapter } from '../../models/testung';
import { TextareaQuestion } from '../../utils/dynamic-forms/form-task-textarea';
import { ChapterForm } from '../../utils/dynamic-forms/form-task-chapter';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Chapter } from '../../utils/dynamic-forms/form-chapter';
import { RadioQuestion } from '../../utils/dynamic-forms/form-task-radio';
import { FormBase } from '../../utils/dynamic-forms/form-base';
import { Injectable }   from '@angular/core';
import { FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { Testung } from 'src/app/models/testung';
import { ReviewChapter } from 'src/app/models/review';

const DEFAULT_OPTIONS = [
  {key: '0',  value: '0'},
  {key: '0,5',  value: '12,5'},
  {key: '1',  value: '25'},
  {key: '1,5',  value: '27,5'},
  {key: '2',  value: '50'},
  {key: '2,5',  value: '62,5'},
  {key: '3',  value: '75'},
  {key: '3,5',  value: '82,5'},
  {key: '4',  value: '100'}
];
const RADIO2_OPTIONS = [
  {key: 'Homolog',  value: 'Homolog'},
  {key: 'Homolateral',  value: 'Homolateral'},
  {key: 'Einseitig',  value: 'Einseitig'},
  {key: 'Desorganisiert',  value: 'Desorganisiert'},
  {key: 'Kreuzmuster',  value: 'Kreuzmuster'}
];
const RADIO3_OPTIONS = [
  {key: 'Homolog',  value: 'Homolog'},
  {key: 'Homolateral',  value: 'Homolateral'},
  {key: 'Desorganisiert',  value: 'Desorganisiert'},
  {key: 'Versetztes Kreuzmuster',  value: 'Versetztes Kreuzmuster'},
  {key: 'Kreuzmuster',  value: 'Kreuzmuster'}
];
const RADIO4_OPTIONS = [
  {key: 'abwesend',  value: 'abwesend'},
  {key: 'schwach präsent',  value: 'schwach präsent'},
  {key: 'präsent',  value: 'präsent'}
];
const RADIOYESNO_OPTIONS = [
  {key: 'Ja',  value: 'Ja'},
  {key: 'Nein',  value: 'Nein'}
];
const RADIOLEFTRIGHT_OPTIONS = [
  {key: 'Links',  value: 'Links'},
  {key: 'Rechts',  value: 'Rechts'}
];

@Injectable()
export class TestungFormControlService {
  constructor(private $http: HttpClient) { }

  toFormGroup(questions: Map<TestungChapter, FormBase<any>[]>) {
    let myform: FormGroup = new FormGroup({});

    for (let chapter of questions.keys()) {

      let array: FormGroup = new FormGroup({});
      questions.get(chapter).forEach(question => {

        var control = question.required ? new FormControl(question.value || '', Validators.required)
                              : new FormControl(question.value || '');
        array.addControl(question.key, control);

      });
      myform.addControl(chapter.id.toString(), array);
    }
    return myform;
  }

  toFormGroupReview(questions: Map<ReviewChapter, FormBase<any>[]>) {
    let myform: FormGroup = new FormGroup({});

    for (let chapter of questions.keys()) {

      let array: FormGroup = new FormGroup({});
      questions.get(chapter).forEach(question => {

        var control = question.required ? new FormControl(question.value || '', Validators.required)
                              : new FormControl(question.value || '');
        array.addControl(question.key, control);

      });
      myform.addControl(chapter.id.toString(), array);
    }
    return myform;
  }

  // getTestungForPatient ( patientId: number ): Promise<Testung> {
  //   // return this.$http.get<Testung>( environment.endpoint + 'Testung/' + patientId )
  //   //            .toPromise();
  // }

  getFormEntries(item: Testung) {

      let chapters: Map<TestungChapter, FormBase<any>[]>  = new Map();
      item.chapters.forEach(chapter => {
        let entries: FormBase<any>[] = [];
        chapter.questions.forEach(question => {
          switch (question.type) {
            case 'radio':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: DEFAULT_OPTIONS,
                  value: question.value
                })
              );
              break;
            case 'textarea':
              entries.push(
                new TextareaQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  value: question.value
                })
              );
              break;
            case 'input':
              entries.push(
                new TextareaQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  value: question.value
                })
              );
              break;
            case 'radio2':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIO2_OPTIONS,
                  value: question.value
                })
              )
              break;
            case 'radio3':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIO3_OPTIONS,
                  value: question.value
                })
              )
              break;
            case 'radio4':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIO4_OPTIONS,
                  value: question.value
                })
              )
              break;
            case 'radioYesNo':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIOYESNO_OPTIONS,
                  value: question.value
                })
              )
              break;
            case 'radioLeftRight':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIOLEFTRIGHT_OPTIONS,
                  value: question.value
                })
              )
              break;
          }
        });
        chapters.set( chapter, entries.sort((a, b) => a.order - b.order) );

      });
      return chapters;
    }

    getFormEntriesReview(revChapters: ReviewChapter[]) {

      let chapters: Map<ReviewChapter, FormBase<any>[]>  = new Map();
      revChapters.forEach(chapter => {
        let entries: FormBase<any>[] = [];
        chapter.questions.forEach(question => {
          switch (question.type) {
            case 'radio':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: DEFAULT_OPTIONS,
                  value: question.value
                })
              );
              break;
            case 'textarea':
              entries.push(
                new TextareaQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  value: question.value
                })
              );
              break;
            case 'input':
              entries.push(
                new TextareaQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  value: question.value
                })
              );
              break;
            case 'radio2':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIO2_OPTIONS,
                  value: question.value
                })
              )
              break;
            case 'radio3':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIO3_OPTIONS,
                  value: question.value
                })
              )
              break;
            case 'radio4':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIO4_OPTIONS,
                  value: question.value
                })
              )
              break;
            case 'radioYesNo':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIOYESNO_OPTIONS,
                  value: question.value
                })
              )
              break;
            case 'radioLeftRight':
              entries.push(
                new RadioQuestion({
                  id: question.id,
                  key: 'question_' + question.id,
                  label: question.label,
                  options: RADIOLEFTRIGHT_OPTIONS,
                  value: question.value
                })
              )
              break;
          }
        });
        chapters.set( chapter, entries.sort((a, b) => a.order - b.order) );

      });
      return chapters;
    }
}
