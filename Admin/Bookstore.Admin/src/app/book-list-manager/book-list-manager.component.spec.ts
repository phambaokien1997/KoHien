import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookListManagerComponent } from './book-list-manager.component';

describe('BookListManagerComponent', () => {
  let component: BookListManagerComponent;
  let fixture: ComponentFixture<BookListManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookListManagerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookListManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
