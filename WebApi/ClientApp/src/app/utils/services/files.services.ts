import { Injectable } from '@angular/core';
import { ApiService } from 'libs/shared/api/src/lib/services';
import { map } from 'rxjs/operators';

@Injectable()
export class FileService {

  private resource = `filedata`;
  private inputFile: FormData = new FormData();

    constructor(private api: ApiService) { }


    upload(file: File) {
      // var input = new FormData();
      this.inputFile.append("filesData", file);
      return this.api.post<FormData>(this.resource, this.inputFile);
    }
}
