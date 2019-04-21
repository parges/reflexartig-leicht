import { FormBase } from './form-base';

export class ChapterForm extends FormBase<string> {
    controlType = 'chapter';

    constructor(options: {} = {}) {
      super(options);
    }
}
