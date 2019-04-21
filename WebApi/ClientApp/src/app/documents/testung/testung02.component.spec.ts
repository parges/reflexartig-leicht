import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Testung02Component } from './testung02.component';

describe('Testung02Component', () => {
  let component: Testung02Component;
  let fixture: ComponentFixture<Testung02Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Testung02Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Testung02Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
