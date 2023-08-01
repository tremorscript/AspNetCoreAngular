import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestExamplesComponent } from './test-examples.component';

describe('TestExamplesComponent', () => {
  let component: TestExamplesComponent;
  let fixture: ComponentFixture<TestExamplesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestExamplesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestExamplesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
