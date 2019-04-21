import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { Injectable, OnDestroy } from '@angular/core';
import { MatSpinner } from '@angular/material';
import { Subject, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService implements OnDestroy {

  private spin$: Subject<boolean> = new Subject();
  private spinnerRef = this._createSpinnerRef();
  private spinSubscription: Subscription;

  constructor(private overlay: Overlay) {
    this.spinSubscription = this.spin$
      .asObservable()
      .subscribe(
        (show: boolean) => {
          if (show) {
            this._showSpinner();
          } else {
            if (this.spinnerRef.hasAttached()) {
              this._stopSpinner();
            }
          }
        }
      );
  }

  public showSpinner(): void {
    this.spin$.next(true);
  }

  public hideSpinner(): void {
    this.spin$.next(false);
  }

  ngOnDestroy(): void {
    if (this.spinSubscription && !this.spinSubscription.closed) {
      this.spinSubscription.unsubscribe();
    }
  }

  private _createSpinnerRef(): OverlayRef {
    return this.overlay.create({
      hasBackdrop: true,
      backdropClass: 'dark-backdrop',
      positionStrategy: this.overlay.position()
        .global()
        .centerHorizontally()
        .centerVertically()
    });
  }

  private _showSpinner(): void {
    this.spinnerRef.attach(new ComponentPortal(MatSpinner));
  }

  private _stopSpinner(): void {
    this.spinnerRef.detach();
  }
}
