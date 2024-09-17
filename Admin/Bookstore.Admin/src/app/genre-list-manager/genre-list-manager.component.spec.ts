import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenreListManagerComponent } from './genre-list-manager.component';

describe('GenreListManagerComponent', () => {
  let component: GenreListManagerComponent;
  let fixture: ComponentFixture<GenreListManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenreListManagerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GenreListManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
