import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SnackbarGenericComponent } from './snackbar-generic.component';

describe('SnackbarGenericComponent', () => {
  let component: SnackbarGenericComponent;
  let fixture: ComponentFixture<SnackbarGenericComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SnackbarGenericComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SnackbarGenericComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
