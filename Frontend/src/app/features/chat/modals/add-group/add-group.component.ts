import { Component, EventEmitter, Input, Output } from '@angular/core';
import { group } from 'src/app/core/models/group';
import { user } from 'src/app/core/models/user';
import { GroupService } from 'src/app/core/services/group.service';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.scss']
})
export class AddGroupComponent {
  @Input() isVisible: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();
  groups!: group[];
  @Input() appUser: user | null = null;

  constructor(
    private userService: UserService,
    private groupService: GroupService) { }

  loadGroups() {
    this.groupService.getGroups().subscribe({
      next: (data) => {
        let groupsId = this.appUser?.groups.map(group => group.id) || []
        this.groups = data.filter(group => !groupsId.includes(group.id))
      }
    })
  }
  filterGroups(value: string) {
    this.groupService.getGroupsByName(value).subscribe({
      next: (res) => {
        this.groups = res;
      }
    })
  }
  joinGroup(groupId: string) {
    let userId = this.appUser?.id;
    this.userService.addToGroup(groupId, userId!).subscribe({
      next: (res) => {
        if (res.status === 204) {
          window.location.reload();
        }
      }
    })
  }
  closeModal() {
    this.closeModalEvent.emit();
  }
}
