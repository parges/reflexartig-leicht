import { Observable } from 'rxjs';
import { ModuleConfigToken } from './../../../../libs/shared/api/src/lib/token';
import { ApiConfig } from './../../../../libs/shared/api/src/lib/interfaces/index';
import { HttpClient } from '@angular/common/http';
import { Customer } from './../../customer/customer';
import { ApiService } from './../../../../libs/shared/api/src/lib/services/api.service';
import { Document } from './../../models/document';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { Component, ViewChild, AfterViewInit, OnInit, Inject } from '@angular/core';
import { saveAs } from 'file-saver';


const ELEMENT_DATA: Document[] = [
  {id: 1, name: 'Fallübersicht'},
  {id: 2, name: 'Fragebogen Kinder'},
  {id: 3, name: 'Testbogen'},
  {id: 4, name: 'Elterngespräch'},
  {id: 5, name: 'Review + Retests'},
];

@Component({
  selector: 'app-overview-table',
  templateUrl: './overview-table.component.html',
  styleUrls: ['./overview-table.component.scss']
})
export class OverviewTableComponent implements OnInit {

  displayedColumns: string[] = ['name', 'action'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  @ViewChild(MatSort, { static: true }) sort: MatSort;

  private resoure = `document`;
  private resourceFile = `file`;

  constructor(@Inject(ModuleConfigToken) private apiConfig: ApiConfig, private $router: Router, private api: ApiService, private http: HttpClient) { }

  ngOnInit() {
    this.dataSource.sort = this.sort;
  }

  openDocument ( _id: string, $event: Event ) {
    this.$router.navigate( ['document/', _id]);
  }


  downloadPDF(_id: string) {
    this.DownloadFile(_id)
    .subscribe(resultBlob =>
      {
        //Success
        // console.log('start download:', resultBlob);
        var blob = new Blob([resultBlob], { type: "application/pdf" } );
        saveAs(blob, "INPP_Formular_"+ _id + Date.now());
      },
    error => {
      //Error
      console.log(error);
    });
  }
  DownloadFile(_id: string): Observable<Blob> {
    const options = { responseType: 'blob' as 'json' };
    return this.http.get<Blob>(`${this.apiConfig.endpoint}/${this.resourceFile}/${_id}`, options);

  }


}
