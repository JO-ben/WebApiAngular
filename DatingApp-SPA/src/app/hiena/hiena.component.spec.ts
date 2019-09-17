/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HienaComponent } from './hiena.component';

describe('HienaComponent', () => {
  let component: HienaComponent;
  let fixture: ComponentFixture<HienaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HienaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HienaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
