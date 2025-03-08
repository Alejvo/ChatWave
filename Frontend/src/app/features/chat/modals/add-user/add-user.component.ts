import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { user } from 'src/app/core/models/user';
import { UserService } from 'src/app/core/services/user.service';
import { FriendService } from '../../services/friend.service';
import { friend } from 'src/app/core/models/friend';
import { ChatService } from '../../services/chat.service';

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
  currentPage: number = 1;
  pageSize: number = 5;
  totalPages :number =0;
  searchTerm : string ='';
  sortColumn : string = 'username';
  sortOrder : string = 'asc';
  constructor(
    private userService: UserService,
    private friendService:FriendService,
    private chatService:ChatService
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
  
  goToPage(page:number){
    if(page >=1 && page <= this.totalPages) this.getUsers(page);
  }

  makeRequest(friendId: string) {
    let userId = this.appUser!.id;
    
    this.friendService.makeFriendRequest(userId, friendId).subscribe({
      next: (res) => {
        this.chatService.notifyRequest(friendId,this.appUser!.username);
        this.chatService.getFriendRequest(friendId);
      }
    })
  }
  
  getUsers(page:number) {
    if(!this.appUser) return;

    this.userService.getUsers(
      page,
      this.pageSize,
      this.appUser!.id,
      this.searchTerm,
      this.sortColumn,
      this.sortOrder).subscribe({
      next: (data) => 
        { 
          this.result = data.items; 
          this.totalPages = data.totalPages;
          this.currentPage = data.page;
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
