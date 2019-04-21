import { environment } from './../../../environments/environment';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { HttpEventType, HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  public progress: number;
  public message: string;
  @Input() userId: number;
  // tslint:disable-next-line:no-output-on-prefix
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient) { }

  ngOnInit() {

  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    // const fileToUpload = <File>files[0];
    // const formData = new FormData();
    // formData.append('file', fileToUpload, fileToUpload.name);

    if (files && files[0]) {
        let fileToUpload = files[0];
        let input = new FormData();
      input.append("file", fileToUpload);

      // this.http.post(environment.endpoint, input)
      //   .subscribe();
    }



    // tslint:disable-next-line:max-line-length
    // this.http.post(environment.endpointUpload, formData, {headers: new HttpHeaders().set('Content-Type', 'multipart/form-data'), reportProgress: true, observe: 'events'})
    //   .subscribe(event => {
    //     if (event.type === HttpEventType.UploadProgress) {
    //       this.progress = Math.round(100 * event.loaded / event.total);
    //     } else if (event.type === HttpEventType.Response) {
    //       this.message = 'Upload success.';
    //       this.onUploadFinished.emit(event.body);
    //     }
    //   });

  }
}
