import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { group } from 'src/app/core/models/group';
import { user } from 'src/app/core/models/user';
import { GroupService } from 'src/app/core/services/group.service';
import { UserService } from 'src/app/core/services/user.service';
import { FriendService } from '../../services/friend.service';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.scss']
})
export class AddGroupComponent  implements OnInit{
  @Input() isVisible: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();
  groups!: group[];
  private appUser: user | null = null;

  constructor(
    private userService: UserService,
    private groupService: GroupService) { }

  ngOnInit(): void {
    this.appUser = this.userService.getUser();
  }
  filterGroups(page: number, pageSize: number) {
    this.groupService.getGroups(page, pageSize).subscribe({
      next: (res) => {
        this.groups = res.items;
      }
    })
  }
  joinGroup(groupId: string) {
    let userId = this.appUser?.id;
    console.log(`${this.appUser?.username} send a friend joined to: ${groupId }`);
    
    this.groupService.joinGroup(groupId!, userId!).subscribe({
      next: (res) => {
        if (res.status === 204) {
          console.log(`${this.appUser?.username} sended a friend request to: ${groupId}`);
        }
      }
    })
  }
  closeModal() {
    this.closeModalEvent.emit();
  }
}
