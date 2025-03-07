import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { user } from 'src/app/core/models/user';
import { UserService } from 'src/app/core/services/user.service';
import { FriendService } from '../../services/friend.service';
import { friend } from 'src/app/core/models/friend';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit{
  @Input() isVisible: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();
  public appUser:user | null = null; 
  result!: user[];
  friendSet:Set<string> = new Set();

  constructor(
    private userService: UserService,
    private friendService:FriendService
  ) { }

  ngOnInit(){
    this.appUser = this.userService.getUser();
    this.friendService.getRequests(this.appUser!.id).subscribe({
      next:(data:friend[])=>{
        console.log(data)
        data.forEach(user => this.friendSet.add(user.id));
      }
    })
  }
  
  makeRequest(friendId: string) {
    let userId = this.appUser?.id;
    
    this.friendService.makeFriendRequest(userId!, friendId).subscribe({
      next: (res) => {
        if (res.status === 204) {
          console.log(`${this.appUser?.username} sended a friend request to: ${friendId}`);
        }
      }
    })
  }
  
  getUsers(page:number,pageSize:number) {
    this.userService.getUsers(page,pageSize,this.appUser!.id).subscribe({
      next: (data) => {
        //console.log(data.items);
        this.result = data.items;
        //let friendId = this.appUser?.friends.map(friend => friend.id) || []
        //this.result = data.filter(user => user.id !== this.appUser?.id && !friendId.includes(user.id))
      }
    })
  }

  closeModal() {
    this.closeModalEvent.emit();
  }
  isRequestSended(id:string):boolean{
    return this.friendSet.has(id);
  }
  isFriend(id:string):boolean{
    return this.appUser!.friends.some((user)=>user.id === id);
  }

  getFriendshipStatus(id: string): 'Add Friend' | 'Is Already Friend' | 'Request Sended' {
    if (this.isFriend(id)) {
      return 'Is Already Friend';
    }
    if (this.isRequestSended(id)) {
      return 'Request Sended';
    }
    return 'Add Friend';
  }
}
