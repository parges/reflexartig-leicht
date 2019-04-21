import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Uebersicht00Component } from './uebersicht00.component';

describe('Uebersicht00Component', () => {
  let component: Uebersicht00Component;
  let fixture: ComponentFixture<Uebersicht00Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Uebersicht00Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Uebersicht00Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
