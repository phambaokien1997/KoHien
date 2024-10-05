import { Component, OnInit, Input } from '@angular/core';
import { CommonModalComponent } from '../../share-component/common-modal/common-modal.component';
import { CommonModule } from '@angular/common';
import { Author } from '../../models/author';
import { AuthorService } from '../../services/author.service';
import { Genre } from '../../models/genre';
import { GenreService } from '../../services/genre.service';
import { Publisher } from '../../models/publisher';
import { PublisherService } from '../../services/publisher.service';
import { Book } from '../../models/book';
import { MatSelectModule } from '@angular/material/select';
import { MetaData } from '../../models/metadata';

@Component({
  selector: 'app-create-update-book-modal',
  standalone: true,
  imports: [CommonModalComponent, CommonModule, MatSelectModule],
  templateUrl: './create-update-book-modal.component.html',
  styleUrl: './create-update-book-modal.component.css',
})
export class CreateUpdateBookModalComponent implements OnInit {
  @Input() book: Book | null = null;
  @Input() isEditMode: boolean = false;
  authorOptions: Author[] = [];
  genreOptions: MetaData[] = [];
  publisherOptions: MetaData[] = [];
  selectedAuthorId: number[] | null = null;
  constructor(
    private authorService: AuthorService,
    private genreService: GenreService,
    private publisherService: PublisherService
  ) {}
  ngOnInit(): void {
    if (this.book) {
      this.selectedAuthorId = this.book.authorIds;
      console.log('Chỉnh sửa sách:', this.book);
    } else {
      console.log('Thêm sách mới');
    }
    this.loadAuthors();
    this.loadGenres();
    this.loadPublisher();
  }
  loadAuthors() {
    this.authorService.getAuthors().subscribe(
      (authors) => {
        console.log(authors);
        this.authorOptions = authors;
      },
      (error) => {
        console.error('Error fetching authors', error);
      }
    );
  }
  loadGenres() {
    // const genres: Genre[] = this.genreService.getGenre();
    // this.genreOptions = genres.map((genre) => genre.name);
  }
  loadPublisher() {
    this.publisherService.getPubliserName().subscribe(
      (publishers) => {
        this.publisherOptions = publishers;
      },
      (error) => {
        console.error('Error fetching authors', error);
      }
    );
  }
  saveBook() {}
  _isSelectedAuthors(otpion: Author) {
    return true;
  }
}
