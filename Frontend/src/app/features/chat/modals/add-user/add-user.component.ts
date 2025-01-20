import { Component, EventEmitter, Input, Output } from '@angular/core';
import { user } from 'src/app/core/models/user';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent {
  @Input() isVisible: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();
  result!: user[];
  @Input() appUser: user | null = null;


  constructor(private userService: UserService) { }

  addFriend(friendId: string) {
    let userId = this.appUser?.id;
    this.userService.addToFriend(userId!, friendId).subscribe({
      next: (res) => {
        if (res.status === 204) {
          window.location.reload();
        }
      }
    })
  }

  filterUsers(value: string) {
    this.userService.getUsersByUsername(value).subscribe({
      next: (data) => {
        let friendId = this.appUser?.friends.map(friend => friend.id) || []
        this.result = data.filter(user => user.id !== this.appUser?.id && !friendId.includes(user.id))
      }
    })
  }

  loadContent() {
    this.userService.getUsers().subscribe({
      next: (data) => {
        let friendId = this.appUser?.friends.map(friend => friend.id) || []
        this.result = data.filter(user => user.id !== this.appUser?.id && !friendId.includes(user.id))

      }
    })
  }

  closeModal() {
    this.closeModalEvent.emit();
  }
}
