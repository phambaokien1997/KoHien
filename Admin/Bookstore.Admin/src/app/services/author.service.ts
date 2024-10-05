import { Injectable } from '@angular/core';
import { Author } from '../models/author';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiUrls } from '../../Common/ApiUrls';
@Injectable({
  providedIn: 'root',
})
export class AuthorService {
  constructor(private http: HttpClient) {}
  // getAuthor(): Author[] {
  //   return [
  //     {
  //       id: 1,
  //       name: 'John Doe',
  //       dateOfBirth: new Date('1980-01-01'),
  //       gender: true,
  //       country: 'USA',
  //       phoneNumber: '123-456-7890',
  //       uniqueId: 'abc123',
  //       createAt: new Date(),
  //       updateAt: new Date(),
  //     },
  //     {
  //       id: 2,
  //       name: 'Jane Smith',
  //       dateOfBirth: new Date('1990-05-15'),
  //       gender: false,
  //       country: 'Canada',
  //       phoneNumber: '987-654-3210',
  //       uniqueId: 'def456',
  //       createAt: new Date(),
  //       updateAt: new Date(),
  //     },
  //     {
  //       id: 3,
  //       name: 'Emily Johnson',
  //       dateOfBirth: new Date('1985-08-22'),
  //       gender: false,
  //       country: 'UK',
  //       phoneNumber: '456-789-0123',
  //       uniqueId: 'ghi789',
  //       createAt: new Date(),
  //       updateAt: new Date(),
  //     },
  //     {
  //       id: 4,
  //       name: 'Michael Brown',
  //       dateOfBirth: new Date('1978-11-30'),
  //       gender: true,
  //       country: 'Australia',
  //       phoneNumber: '321-654-9870',
  //       uniqueId: 'jkl012',
  //       createAt: new Date(),
  //       updateAt: new Date(),
  //     },
  //     {
  //       id: 5,
  //       name: 'Sarah Davis',
  //       dateOfBirth: new Date('1992-03-10'),
  //       gender: false,
  //       country: 'New Zealand',
  //       phoneNumber: '654-321-0987',
  //       uniqueId: 'mno345',
  //       createAt: new Date(),
  //       updateAt: new Date(),
  //     },
  //   ];
  // }
  getAuthors(): Observable<Author[]> {
    return this.http.get<Author[]>(ApiUrls.Author.getAuthor);
  }
}
