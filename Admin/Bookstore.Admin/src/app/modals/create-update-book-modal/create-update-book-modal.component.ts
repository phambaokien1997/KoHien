import { Component, OnInit } from '@angular/core';
import { CommonModalComponent } from '../../share-component/common-modal/common-modal.component';
import { CommonModule } from '@angular/common';
import { Author } from '../../models/author';
import { AuthorService } from '../../services/author.service';
import { Genre } from '../../models/genre';
import { GenreService } from '../../services/genre.service';
import { Publisher } from '../../models/publisher';
import { PublisherService } from '../../services/publisher.service';
@Component({
  selector: 'app-create-update-book-modal',
  standalone: true,
  imports: [CommonModalComponent, CommonModule],
  templateUrl: './create-update-book-modal.component.html',
  styleUrl: './create-update-book-modal.component.css'
})
export class CreateUpdateBookModalComponent implements OnInit {
  authorOptions : string [] = [];
  genreOptions : string [] =[];
  publisherOptions : string [] = [];
  constructor(private authorService : AuthorService, private genreService : GenreService, private publisherService : PublisherService){
  }
  ngOnInit(): void {
    this.loadAuthors();
    this.loadGenres();
    this.loadPublisher();
  }
  loadAuthors() {
    const authors: Author[] = this.authorService.getAuthor();
    this.authorOptions = authors.map(author => author.name);
  }
  loadGenres(){
    const genres: Genre[] = this.genreService.getGenre();
    this.genreOptions = genres.map(genre => genre.name);
  }
  loadPublisher(){
    const publishers: Publisher[] = this.publisherService.getPublisher();
    this.publisherOptions = publishers.map(publisher => publisher.name);
  }
}
