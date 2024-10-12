import { Routes } from '@angular/router';
import { BookListManagerComponent } from './book-list-manager/book-list-manager.component';
import { AuthorListManagerComponent } from './author-list-manager/author-list-manager.component';
import { GenreListManagerComponent  } from './genre-list-manager/genre-list-manager.component';
import { PublisherListManagerComponent } from './publisher-list-manager/publisher-list-manager.component';

// Định nghĩa các routes cho ứng dụng
export const routes: Routes = [
  { path: 'book-list-manager', component: BookListManagerComponent },
  { path: 'author-list-manager', component: AuthorListManagerComponent },
  { path: 'genre-list-manager', component: GenreListManagerComponent },
  { path: 'publisher-list-manager', component: PublisherListManagerComponent },
  { path: '', redirectTo: '/book-list-manager', pathMatch: 'full' }  // Mặc định chuyển hướng đến page1
];