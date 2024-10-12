import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherListManagerComponent } from './publisher-list-manager.component';

describe('PublisherListManagerComponent', () => {
  let component: PublisherListManagerComponent;
  let fixture: ComponentFixture<PublisherListManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PublisherListManagerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PublisherListManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
