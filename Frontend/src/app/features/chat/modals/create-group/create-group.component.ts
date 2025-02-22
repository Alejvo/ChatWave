import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { GroupService } from 'src/app/core/services/group.service';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.scss']
})
export class CreateGroupComponent {
  @Input() isVisible: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();
  @ViewChild('createGroupForm') form !: NgForm;
  image: File | null = null

  constructor(private groupService: GroupService) { }

  public createGroup(form:NgForm) {
    const { name,description } = form.value;
    this.groupService.createGroup(
      name, 
      description,
      this.image!
    ).subscribe()
    this.form.reset();
    this.closeModal();
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.image = input.files[0];
    }
  }

  closeModal() {
    this.closeModalEvent.emit();
  }
}
