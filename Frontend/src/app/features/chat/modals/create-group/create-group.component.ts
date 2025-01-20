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
  groupRequest = {
    name: '',
    description: ''
  };
  constructor(private groupService: GroupService) { }
  public createGroup() {
    this.groupService.createGroup(this.groupRequest.name, this.groupRequest.description).subscribe()
    this.groupRequest.name = '';
    this.groupRequest.description = '';
    this.closeModal();
  }
  closeModal() {
    this.closeModalEvent.emit();
  }
}
