import { FormBase } from './form-base';

export class InputQuestion extends FormBase<string> {
    controlType = 'input';
    type: string;

    constructor(options: {} = {}) {
      super(options);
      this.type = options['input'] || '';
    }
}
