import { Component, OnInit } from '@angular/core';
import { ReusableTableComponent } from '../share-component/reusable-table/reusable-table.component';
import { Book } from '../models/book';
import { BookService } from '../services/book.service';
import { CommonModule } from '@angular/common';
import { CommonModalComponent } from '../share-component/common-modal/common-modal.component';
import { CreateUpdateBookModalComponent } from '../modals/create-update-book-modal/create-update-book-modal.component';

@Component({
  selector: 'app-book-list-manager',
  standalone: true,
  imports: [
    ReusableTableComponent,
    CommonModule,
    CommonModalComponent,
    CreateUpdateBookModalComponent,
  ],
  templateUrl: './book-list-manager.component.html',
  styleUrl: './book-list-manager.component.css',
})
export class BookListManagerComponent implements OnInit {
  books: Book[] = [];
  options: string[] = [];
  selectedOption: string = '';
  isModalOpen = false;
  isEditMode = false;
  text: string = 'hello';
  headers: string[] = [
    'Name',
    'Title',
    'Short Description',
    'Authors',
    'Price',
    'Quantity',
    'Publication Date',
    'Genre',
    'Publisher',
    'Created At',
  ];
  selectedBook: Book | null = null;
  selectedBookId: number | null = null;
  constructor(private bookService: BookService) {
    // this.books = bookService.getBooks();
  }
  loadBooks(): void {
    this.bookService.getBooks().subscribe({
      next: (data: Book[]) => {
        this.books = data; // Lưu dữ liệu vào biến books
      },
      error: (error) => {
        console.error('Lỗi khi lấy sách:', error);
      },
      complete: () => {
        console.log('Tải sách hoàn tất.');
      },
    });
  }
  loadBookDetails(id: number): void {
    this.bookService.getBookById(id).subscribe({
      next: (book: Book) => {
        this.selectedBook = book;
      },
      error: (error) => {
        console.log('Lỗi khi lấy thông tin sách', error);
      },
      complete: () => {
        console.log('Tải sách hoàn tất');
      },
    });
  }
  ngOnInit(): void {
    this.options = ['Option 1', 'Option 2', 'Option 3', 'Option 4'];
    this.loadBooks();
  }

  onEdit(id: number) {
    this.selectedBookId = id;
    // Handle edit action
    console.log(`Edit book with ID ${id}`);
    this.loadBookDetails(this.selectedBookId);
    this.isEditMode = true;
    this.isModalOpen = true;
  }

  onDelete(id: number) {
    // Handle delete action
    console.log(`Delete book with ID ${id}`);
  }
  openModal() {
    this.selectedBook = null;
    this.isEditMode = false;
    this.isModalOpen = true;
  }
  closeModal() {
    this.isModalOpen = false; // Đóng modal
  }
}
