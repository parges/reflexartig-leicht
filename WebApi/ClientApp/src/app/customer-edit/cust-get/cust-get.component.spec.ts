import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustGetComponent } from './cust-get.component';

describe('CustGetComponent', () => {
  let component: CustGetComponent;
  let fixture: ComponentFixture<CustGetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustGetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustGetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
