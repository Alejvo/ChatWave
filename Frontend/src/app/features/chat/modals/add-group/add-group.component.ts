import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { group } from 'src/app/core/models/group';
import { user } from 'src/app/core/models/user';
import { GroupService } from 'src/app/core/services/group.service';
import { UserService } from 'src/app/core/services/user.service';
import { FriendService } from '../../services/friend.service';
import { ChatService } from '../../services/chat.service';

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
  currentPage: number = 1;
  pageSize: number = 3;
  totalPages: number = 0;
  searchTerm: string = '';
  sortColumn: string = 'name';
  sortOrder: string = 'asc';

  constructor(
    private userService: UserService,
    private groupService: GroupService,
    private chatService:ChatService) { }

  ngOnInit(): void {
    this.appUser = this.userService.getUser();
  }

  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages) this.getGroups(page);
  }

  getGroups(page: number) {
    if(!this.appUser) return;

    this.groupService.getGroups(
      page, 
      this.pageSize,
      this.searchTerm,
      this.sortColumn,
      this.sortOrder
    ).subscribe({
      next: (res) => {
        this.groups = res.items;
        this.totalPages = res.totalPages;
        this.currentPage = res.page;
      }
    })
  }

  joinGroup(groupId: string) {
    let userId = this.appUser?.id;
    
    this.groupService.joinGroup(groupId!, userId!).subscribe({
      next: (res) => {
        if (res.status === 204) {
          this.chatService.getGroupList(this.appUser!.id);
        }
      }
    })

  }
  closeModal() {
    this.closeModalEvent.emit();
  }
}
