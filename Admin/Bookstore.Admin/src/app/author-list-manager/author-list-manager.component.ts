import { Component, OnInit } from '@angular/core';
import { ReusableTableComponent } from '../share-component/reusable-table/reusable-table.component';
import { Author } from '../models/author';
import { CommonModule } from '@angular/common';
import { AuthorService } from '../services/author.service';
@Component({
  selector: 'app-author-list-manager',
  standalone: true,
  imports: [ReusableTableComponent, CommonModule],
  templateUrl: './author-list-manager.component.html',
  styleUrl: './author-list-manager.component.css',
})
export class AuthorListManagerComponent implements OnInit {
  authors: Author[] = [];
  headers: string[] = [
    'id',
    'name',
    'dateOfBirth',
    'gender',
    'country',
    'phoneNumber',
    'uniqueId',
    'createAt',
    'updateAt',
  ];
  constructor(private authorService: AuthorService) {}
  ngOnInit(): void {
    this.loadAuthors();
  }
  loadAuthors(): void {
    this.authorService.getAuthors().subscribe({
      next: (data: Author[]) => {
        this.authors = data; // Lưu dữ liệu vào biến authors
      },
      error: (error) => {
        console.error('Lỗi khi lấy tác giả:', error);
      },
      complete: () => {
        console.log('Tải tác giả hoàn tất.');
      },
    });
  }

  onEdit(id: number) {
    console.log(`Edit author with ID ${id}`);
  }
  onDelete(id: number) {
    console.log(`Delete author with ID ${id}`);
  }
}
