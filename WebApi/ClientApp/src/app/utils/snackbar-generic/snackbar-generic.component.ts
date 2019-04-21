import { Component, OnInit, Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-snackbar-generic',
  templateUrl: './snackbar-generic.component.html',
  styleUrls: ['./snackbar-generic.component.scss']
})
@Injectable({
  providedIn: 'root'
})
export class SnackbarGenericComponent {

  constructor(private snackBar: MatSnackBar) {}

  openSnackBarAction(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000
    });
  }
  openSnackBar(message: string) {
    this.snackBar.open(message, null, {
      duration: 2000
    });
  }

}
