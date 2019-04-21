import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestungChildrenComponent } from './testungChildren.component';

describe('TestungChildrenComponent', () => {
  let component: TestungChildrenComponent;
  let fixture: ComponentFixture<TestungChildrenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestungChildrenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestungChildrenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
