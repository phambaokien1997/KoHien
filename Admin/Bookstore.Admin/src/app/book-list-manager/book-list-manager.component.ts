import { Component, OnInit } from '@angular/core';
import { ReusableTableComponent } from "../share-component/reusable-table/reusable-table.component";
import { Book } from '../models/book';
import { BookService } from '../services/book.service';
import { CommonModule } from '@angular/common';
import { CommonModalComponent } from '../share-component/common-modal/common-modal.component';
import { CreateUpdateBookModalComponent } from '../modals/create-update-book-modal/create-update-book-modal.component';



@Component({
  selector: 'app-book-list-manager',
  standalone: true,
  imports: [ReusableTableComponent, CommonModule, CommonModalComponent, CreateUpdateBookModalComponent],
  templateUrl: './book-list-manager.component.html',
  styleUrl: './book-list-manager.component.css'
})
export class BookListManagerComponent implements OnInit {
  books: Book[] = [];
  options: string[] = [];
  selectedOption: string = '';
  isModalOpen = false;
  text : string = "hello";
  headers: string[] = [
    'id', 'name', 'title', 'shortDescription', 'authorId', 'price', 'quantity',
    'publicationDate', 'genreId', 'publisherId', 'uniqueId', 'createAt', 'updateAt'
  ];

  constructor(bookService: BookService) {
      this.books = bookService.getBooks();
  }

  ngOnInit(): void {
    this.options = ['Option 1', 'Option 2', 'Option 3', 'Option 4'];
  }

  onEdit(id: number) {
    // Handle edit action
    console.log(`Edit book with ID ${id}`);
  }

  onDelete(id: number) {
    // Handle delete action
    console.log(`Delete book with ID ${id}`);
  }
  openModal(){
    this.isModalOpen = true;
  }
  closeModal() {
    this.isModalOpen = false; // Đóng modal
  }
}