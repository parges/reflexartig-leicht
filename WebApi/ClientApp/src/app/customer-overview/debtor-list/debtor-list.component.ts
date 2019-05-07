import { MatPaginator, MatSort } from '@angular/material';
import { Customer } from './../../customer/customer';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '@rl/shared/api';
import { LoaderService } from 'libs/shared/ui/services';
import { map, catchError } from 'rxjs/operators';
import { ApiResponse } from '@rl/shared/models';
import { of } from 'rxjs';

@Component({
  selector: 'app-debtor-list',
  templateUrl: './debtor-list.component.html',
  styleUrls: ['./debtor-list.component.scss']
})
export class DebtorListComponent implements AfterViewInit {

  displayedColumns: string[] = ['firstname', 'lastname', 'tele', 'address'];
  // dataSource: MatTableDataSource<Customer>[] = [];
  dataSource: Customer[];

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
    this.api.getAction<Customer>(this.resource, "GetAllDebtors")
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
      this.dataSource = data;
    });
  }
  // applyFilter(filterValue: string) {
  //   this.dataSource.filter = filterValue.trim().toLowerCase();
  // }
  showCustomerDetails ( model: Customer, $event: Event ) {
    // this.$router.navigate( ['document/1', model.id]);
    this.$router.navigate( ['document/1', {patientId: model.id}]);
  }
}
