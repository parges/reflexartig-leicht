import { TileGroup } from './../../../libs/shared/models/src/lib/interfaces/interfaces.common';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { Component, ChangeDetectionStrategy } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'dvz-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent {

  public tileGroups: TileGroup[] = [
    {
      title: 'Patienten',
      tiles: [
        {
          title: 'Patienten anzeigen',
          route: ['customers']
        },
        {
          title: 'Patient anlegen',
          route: ['customers/add']
        }
      ]
    },
    {
      title: 'Dokumente',
      tiles: [
        {
          title: 'Dokumente',
          route: ['documents']
        }
      ]
    }
  ];

  public $isHandset: Observable<boolean> = this.breakpointObserver
    .observe(Breakpoints.Handset)
    .pipe(map(result => result.matches));

  constructor(public breakpointObserver: BreakpointObserver) { }
}


// import { Router } from '@angular/router';
// import { Component, OnInit } from '@angular/core';

// @Component({
//   selector: 'app-dashboard',
//   templateUrl: './dashboard.component.html',
//   styleUrls: ['./dashboard.component.scss']
// })
// export class DashboardComponent {

//   constructor(private $router: Router) { }

//   navigate(path: string) {
//     this.$router.navigate([path]);
//   }
// }
