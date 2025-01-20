import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})
export class ContactsComponent {
  @Input() Name!: string;
  @Input() isActive: boolean = false;
  @Input() profilePhoto: string = "";
  @Output() messagesEvent = new EventEmitter<void>();
  @Output() selectedContact = new EventEmitter<string>();

  getMessages() {
    this.messagesEvent.emit();
  }
}
