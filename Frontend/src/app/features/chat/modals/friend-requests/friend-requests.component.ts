import { Component, EventEmitter, Input, Output } from '@angular/core';
import { user } from 'src/app/core/models/user';
import { FriendService } from '../../services/friend.service';
import { UserService } from 'src/app/core/services/user.service';
import { friend } from 'src/app/core/models/friend';

@Component({
  selector: 'app-friend-requests',
  templateUrl: './friend-requests.component.html',
  styleUrls: ['./friend-requests.component.scss']
})
export class FriendRequestsComponent {
  @Input() isVisible: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();
  public appUser:user | null = null; 
  result!: friend[];

  constructor(
    private userService: UserService,
    private friendService:FriendService
  ) { }

  ngOnInit(){
    this.appUser = this.userService.getUser();
    if (this.appUser) {
      setTimeout(() => this.getRequests());
    }
  }
  acceptRequest(friendId:string){
    this.friendService.addFriend(this.appUser!.id,friendId).subscribe();
    this.result.filter(item => item.id !== friendId)
  }  
  getRequests() {
    this.friendService.getRequests(this.appUser!.id).subscribe({
      next:(data)=>{
        this.result = data;
      }
    })
  }

  closeModal() {
    this.closeModalEvent.emit();
  }

  isFriend(id:string):boolean{
    return this.appUser!.friends.some((user)=>user.id === id);
  }
}
