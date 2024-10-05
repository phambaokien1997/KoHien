import {
  Injectable,
  provideExperimentalZonelessChangeDetection,
} from '@angular/core';
import { Genre } from '../models/genre';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class GenreService {
  constructor(private http: HttpClient) {}
  getGenre(): Genre[] {
    return [
      {
        id: 1,
        name: 'Science Fiction',
        description:
          'Genre dealing with futuristic concepts such as advanced science and technology.',
        uniqueId: 'sci-fi-001',
        createAt: new Date(),
        updateAt: new Date(),
      },
      {
        id: 2,
        name: 'Mystery',
        description:
          'Genre involving the solution of a crime or unraveling of a puzzle.',
        uniqueId: 'myst-002',
        createAt: new Date(),
        updateAt: new Date(),
      },
      {
        id: 3,
        name: 'Fantasy',
        description: 'Genre featuring magical elements and imaginary worlds.',
        uniqueId: 'fant-003',
        createAt: new Date(),
        updateAt: new Date(),
      },
      {
        id: 4,
        name: 'Romance',
        description:
          'Genre centered around romantic relationships and emotional experiences.',
        uniqueId: 'rom-004',
        createAt: new Date(),
        updateAt: new Date(),
      },
      {
        id: 5,
        name: 'Historical',
        description:
          'Genre that is set in a specific historical period and reflects historical events.',
        uniqueId: 'hist-005',
        createAt: new Date(),
        updateAt: new Date(),
      },
    ];
  }
}
