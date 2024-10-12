import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorListManagerComponent } from './author-list-manager.component';

describe('AuthorListManagerComponent', () => {
  let component: AuthorListManagerComponent;
  let fixture: ComponentFixture<AuthorListManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthorListManagerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthorListManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
