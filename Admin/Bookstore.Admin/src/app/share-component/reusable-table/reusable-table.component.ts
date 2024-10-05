import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-reusable-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reusable-table.component.html',
  styleUrl: './reusable-table.component.css',
})
export class ReusableTableComponent {
  @Input() headers: string[] = [];
  @Input() rows: any[] = [];
  @Output() edit = new EventEmitter<number>();
  @Output() delete = new EventEmitter<number>();
  onEdit(id: number) {
    this.edit.emit(id);
  }
  onDelete(id: number) {
    this.delete.emit(id);
  }
  formatHeader(header: string): string {
    // Chuyển chữ cái đầu thành chữ thường và loại bỏ khoảng trắng
    return header.charAt(0).toLowerCase() + header.slice(1).replace(/ /g, '');
  }
}
