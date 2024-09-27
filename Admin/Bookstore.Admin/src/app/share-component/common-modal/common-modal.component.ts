import { Component, EventEmitter, Input, input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateUpdateBookModalComponent } from '../../modals/create-update-book-modal/create-update-book-modal.component';
@Component({
  selector: 'common-modal',
  standalone: true,
  imports: [CommonModule, CreateUpdateBookModalComponent],
  templateUrl: './common-modal.component.html',
  styleUrl: './common-modal.component.css',
})
export class CommonModalComponent {
  @Input() isOpen: boolean = false;
  @Input() modalTitle: string = '';
  @Output() closeModal = new EventEmitter<void>(); //Đóng modal

  onClose() {
    this.closeModal.emit();
  }

  onSave() {
    alert('Saved!');
  }
}
