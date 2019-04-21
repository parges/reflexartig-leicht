import { FormBase } from './form-base';

export class Chapter extends FormBase<string> {
    controlType = 'chapter';
    options: {value: string}[] = [];
  
    constructor(options: {} = {}) {
      super(options);
      this.options = options['options'] || [];
    }
}