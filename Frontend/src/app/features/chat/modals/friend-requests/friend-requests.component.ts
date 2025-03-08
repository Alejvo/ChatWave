import { Component, EventEmitter, Input, Output } from '@angular/core';
import { user } from 'src/app/core/models/user';
import { FriendService } from '../../services/friend.service';
import { UserService } from 'src/app/core/services/user.service';
import { friend } from 'src/app/core/models/friend';
import { ChatService } from '../../services/chat.service';

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
    private friendService:FriendService,
    private chatService:ChatService,
  ) { }

  ngOnInit(){
    this.appUser = this.userService.getUser();
    if (this.appUser) {
      setTimeout(() => this.getRequests());
    }
  }
  acceptRequest(senderId:string){
    this.friendService.addFriend(senderId, this.appUser!.id).subscribe(
      {next:(response)=>{
        if(response.status === 204){
          this.chatService.getFriendList(senderId);
          this.chatService.getFriendList(this.appUser!.id);
        }
      }}
    );
    this.result  = this.result.filter(item => item.id !== senderId)
  }  
  getRequests() {
    this.chatService.friendRequest$.subscribe({
      next:(data)=>{
        this.result = data;
    }})
  }

  closeModal() {
    this.closeModalEvent.emit();
  }

  isFriend(id:string):boolean{
    return this.appUser!.friends.some((user)=>user.id === id);
  }
}