import { LoaderService } from './../../../../libs/shared/ui/services/loader.service';
import { ApiResponse } from './../../../../libs/shared/models/src/lib/interfaces/interfaces.common';
import { ApiService } from './../../../../libs/shared/api/src/lib/services/api.service';
import { Customer } from 'src/app/customer/customer';
import {Component, ViewChild, AfterViewInit, HostListener} from '@angular/core';
import {MatPaginator, MatSort} from '@angular/material';
import {merge, of as observableOf, of} from 'rxjs';
import {catchError, map, startWith, switchMap} from 'rxjs/operators';
import { Router, NavigationExtras } from '@angular/router';


@Component({
  selector: 'app-cust-list',
  templateUrl: './cust-list.component.html',
  styleUrls: ['./cust-list.component.scss']
})
export class CustListComponent implements AfterViewInit {

  displayedColumns: string[] = ['firstname', 'lastname', 'tele'];
  dataSource: Customer[] = [];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  private resource = `patient`;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private $router: Router,  private api: ApiService, private loader: LoaderService) {
  }

  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);


    this.api.get<Customer>(this.resource)
    .pipe(
      map((response: ApiResponse<Customer>) => {
        this.loader.hideSpinner();
        this.isLoadingResults = false;
        this.isRateLimitReached = false;
        this.resultsLength = response.totalRecords;
        return response.items;
      }),
      catchError(() => {
        this.loader.hideSpinner();
        this.isLoadingResults = false;
          this.isRateLimitReached = true;
        return of([]);
      })
    )
    .subscribe((data: Customer[]) => {
      this.dataSource = data
    });

    // merge(this.sort.sortChange, this.paginator.page)
    //   .pipe(
    //     startWith({}),
    //     switchMap(() => {
    //       this.isLoadingResults = true;
    //       return this.$customer.getUsers(this.sort.active, this.sort.direction, this.paginator.pageIndex);
    //     }),
    //     map(data => {
    //       // Flip flag to show that loading has finished.
    //       this.isLoadingResults = false;
    //       this.isRateLimitReached = false;
    //       this.resultsLength = this.$customer.totalCount;
    //       return data;
    //     }
    //     ),
    //     catchError(() => {
    //       this.isLoadingResults = false;
    //       this.isRateLimitReached = true;
    //       return observableOf([]);
    //     })
    //   ).subscribe(resp => {
    //     this.dataSource = resp;
    //   });

  }

  showCustomerDetails ( model: Customer, $event: Event ) {
    this.$router.navigate( ['customers/', model.id]);
  }
}
