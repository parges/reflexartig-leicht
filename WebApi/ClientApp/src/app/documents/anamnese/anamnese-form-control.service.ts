import { InputQuestion } from './../../utils/dynamic-forms/form-task-input';
import { AnamneseChapter, Anamnese } from './../../models/anamnese';
import { TextareaQuestion } from '../../utils/dynamic-forms/form-task-textarea';
import { HttpClient } from '@angular/common/http';
import { RadioQuestion } from '../../utils/dynamic-forms/form-task-radio';
import { FormBase } from '../../utils/dynamic-forms/form-base';
import { Injectable }   from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Testung } from 'src/app/models/testung';

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
export class AnamneseFormControlService {
  constructor(private $http: HttpClient) { }

  toFormGroup(questions: Map<AnamneseChapter, FormBase<any>[]>) {
    let myform: FormGroup = new FormGroup({});

    for (let chapter of questions.keys()) {

      let array: FormGroup = new FormGroup({});
      questions.get(chapter).forEach(question => {

        var control = question.required ? new FormControl(question.value || '', Validators.required)
                              : new FormControl(question.value || '');
        array.addControl(question.key, control);
        if(question.textPrefix !== '') {
          var metaControl = question.required ? new FormControl(question.value || '', Validators.required)
                              : new FormControl(question.value || '');
          array.addControl(question.key+'_meta', metaControl);
        }

      });
      myform.addControl(chapter.id.toString(), array);
    }
    return myform;
  }

  // getTestungForPatient ( patientId: number ): Promise<Testung> {
  //   // return this.$http.get<Testung>( environment.endpoint + 'Testung/' + patientId )
  //   //            .toPromise();
  // }

  getFormEntries(item: Anamnese) {

      let chapters: Map<AnamneseChapter, FormBase<any>[]>  = new Map();
      item.chapters.forEach(chapter => {
        let entries: FormBase<any>[] = [];
        chapter.questions.forEach(question => {
          var types: string[] = [];
          if(question.type.indexOf(', ') >= 0) {
            types = question.type.split(', ');
          } else {
            types[0] = question.type;
          }
            switch (types[0]) {
              case 'radio':
                entries.push(
                  new RadioQuestion({
                    key: 'question_' + question.id,
                    label: question.label,
                    options: DEFAULT_OPTIONS,
                    value: question.value,
                    textPrefix: question.textPrefix,
                    textValue: question.textValue,
                    metaInfo: question.metaInfo
                  })
                );
              break;
              case 'textarea':
                entries.push(
                  new TextareaQuestion({
                    key: 'question_' + question.id,
                    label: question.label,
                    value: question.value,
                    textPrefix: question.textPrefix,
                    textValue: question.textValue,
                    metaInfo: question.metaInfo
                  })
                );
                break;
              case 'input':
                entries.push(
                  new InputQuestion({
                    key: 'question_' + question.id,
                    label: question.label,
                    value: question.value,
                    textPrefix: question.textPrefix,
                    textValue: question.textValue,
                    metaInfo: question.metaInfo
                  })
                );
                break;
              case 'radio2':
                entries.push(
                  new RadioQuestion({
                    key: 'question_' + question.id,
                    label: question.label,
                    options: RADIO2_OPTIONS,
                    value: question.value,
                    textPrefix: question.textPrefix,
                    textValue: question.textValue,
                    metaInfo: question.metaInfo
                  })
                )
                break;
              case 'radio3':
                entries.push(
                  new RadioQuestion({
                    key: 'question_' + question.id,
                    label: question.label,
                    options: RADIO3_OPTIONS,
                    value: question.value,
                    textPrefix: question.textPrefix,
                    textValue: question.textValue,
                    metaInfo: question.metaInfo
                  })
                )
                break;
              case 'radio4':
                entries.push(
                  new RadioQuestion({
                    key: 'question_' + question.id,
                    label: question.label,
                    options: RADIO4_OPTIONS,
                    value: question.value,
                    textPrefix: question.textPrefix,
                    textValue: question.textValue,
                    metaInfo: question.metaInfo
                  })
                )
                break;
              case 'radioYesNo':
                entries.push(
                  new RadioQuestion({
                    key: 'question_' + question.id,
                    label: question.label,
                    options: RADIOYESNO_OPTIONS,
                    value: question.value,
                    textPrefix: question.textPrefix,
                    textValue: question.textValue,
                    metaInfo: question.metaInfo
                  })
                )
                break;
              case 'radioLeftRight':
                entries.push(
                  new RadioQuestion({
                    key: 'question_' + question.id,
                    label: question.label,
                    options: RADIOLEFTRIGHT_OPTIONS,
                    value: question.value,
                    textPrefix: question.textPrefix,
                    textValue: question.textValue,
                    metaInfo: question.metaInfo
                  })
                )
                break;
            }
          // }
          // else if(types.length == 2) {

          // }
        });
        chapters.set( chapter, entries.sort((a, b) => a.order - b.order) );

      });
      return chapters;
    }
}
