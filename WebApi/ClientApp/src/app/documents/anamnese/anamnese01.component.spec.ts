import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Anamnese01Component } from './anamnese01.component';

describe('Anamnese01Component', () => {
  let component: Anamnese01Component;
  let fixture: ComponentFixture<Anamnese01Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Anamnese01Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Anamnese01Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
