import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../models/book';
import { Observable } from 'rxjs';
import { ApiUrls } from '../../Common/ApiUrls';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  constructor(private http: HttpClient) {}

  // getBooks(): Book[] {
  //   return [
  //     {
  //       id: 1,
  //       name: 'The Great Gatsby',
  //       title: 'Gatsby',
  //       authorName: 'fuck',
  //       shortDescription: 'A novel set in the 1920s.',
  //       authorId: 101,
  //       price: 10.99,
  //       quantity: 5,
  //       publicationDate: new Date('1925-04-10'),
  //       genreId: 202,
  //       publisherId: 303,
  //       uniqueId: 'unique-001',
  //       createAt: new Date('2024-01-15T08:00:00Z'),
  //       updateAt: new Date('2024-09-01T12:00:00Z'),
  //     },
  //     {
  //       id: 2,
  //       name: '1984',
  //       authorName: 'fuck',
  //       title: 'Dystopian Future',
  //       shortDescription: 'A chilling depiction of a totalitarian regime.',
  //       authorId: 102,
  //       price: 12.5,
  //       quantity: 8,
  //       publicationDate: new Date('1949-06-08'),
  //       genreId: 203,
  //       publisherId: 304,
  //       uniqueId: 'unique-002',
  //       createAt: new Date('2024-02-20T09:00:00Z'),
  //       updateAt: new Date('2024-09-02T14:00:00Z'),
  //     },
  //     {
  //       id: 3,
  //       name: 'To Kill a Mockingbird',
  //       authorName: 'fuck',
  //       title: 'Mockingbird',
  //       shortDescription: 'Story about racial injustice in the Deep South.',
  //       authorId: 103,
  //       price: 8.75,
  //       quantity: 12,
  //       publicationDate: new Date('1960-07-11'),
  //       genreId: 204,
  //       publisherId: 305,
  //       uniqueId: 'unique-003',
  //       createAt: new Date('2024-03-25T10:30:00Z'),
  //       updateAt: new Date('2024-09-03T16:00:00Z'),
  //     },
  //     {
  //       id: 4,
  //       name: 'Pride and Prejudice',
  //       authorName: 'fuck',
  //       title: 'Prejudice',
  //       shortDescription: 'A classic romance novel by Jane Austen.',
  //       authorId: 104,
  //       price: 14.2,
  //       quantity: 7,
  //       publicationDate: new Date('1813-01-28'),
  //       genreId: 205,
  //       publisherId: 306,
  //       uniqueId: 'unique-004',
  //       createAt: new Date('2024-04-30T11:45:00Z'),
  //       updateAt: new Date('2024-09-04T18:30:00Z'),
  //     },
  //     {
  //       id: 5,
  //       name: 'The Catcher in the Rye',
  //       title: 'Catcher',
  //       authorName: 'fuck',
  //       shortDescription: 'A story about teenage angst and alienation.',
  //       authorId: 105,
  //       price: 11.0,
  //       quantity: 6,
  //       publicationDate: new Date('1951-07-16'),
  //       genreId: 206,
  //       publisherId: 307,
  //       uniqueId: 'unique-005',
  //       createAt: new Date('2024-05-15T13:00:00Z'),
  //       updateAt: new Date('2024-09-05T20:00:00Z'),
  //     },
  //   ];
  // }
  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(ApiUrls.Book.baseUrl);
  }
  getBookById(id: number): Observable<Book> {
    return this.http.get<Book>(ApiUrls.Book.baseUrl + '/' + id);
  }
}
