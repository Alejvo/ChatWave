import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { message } from 'src/app/core/models/message';
import { user } from 'src/app/core/models/user';
import { UserService } from 'src/app/core/services/user.service';
import { ChatService } from '../../services/chat.service';
import { AuthService } from 'src/app/features/auth/services/auth.service';
import { group } from 'src/app/core/models/group';
import { friend } from 'src/app/core/models/friend';

@Component({
  selector: 'app-chat-page',
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.scss']
})
export class ChatPageComponent implements OnInit{
  isModalVisible: boolean = false;

  user: user | null = null;
  groups: group[] = [];
  friends: friend[] = [];
  
  isLoading:boolean = true;
  
  messages!: message[];
  messageContent = "";
  currentContact = "";
  contactType !: string;
  isSideBarActive: boolean = false;
  isGroupsAccordionOpen: boolean = true;
  isFriendsAccordionOpen: boolean = true;
  isSent: boolean = true;
  @ViewChild('messageContainer') private messageContainer!: ElementRef;

  constructor(
    public userService: UserService,
    private chatService: ChatService,
    private authService: AuthService) { }

  
  ngOnInit() {

    const token = this.authService.getUserInfoFromToken();

    this.userService.getUserById(token!.userId).subscribe({
      next: (data: user) => {
        this.userService.setUser(data);
        this.user = data;
        this.isLoading = false;
        this.chatService.startConnection().then(() => {
          this.chatService.getGroupList(this.user!.id);
          this.chatService.getFriendList(this.user!.id);
          this.chatService.getFriendRequest(this.user!.id);
        })

      },
      error: (error) => console.error('Error when trying to get user', error)
    });

    
    this.chatService.messageReceived$.subscribe(data => this.messages.push(data));
    this.chatService.messageHistory$.subscribe({
      next: (data) => {
        this.messages = data;
        this.scrollToBottom();
      }
    });

    this.chatService.friends$.subscribe(data => this.friends = data);
    this.chatService.groups$.subscribe(data => this.groups = data);
  }

  showModal() {
    this.isModalVisible = !this.isModalVisible;
  }
  
  ngAfterViewInit() {
    this.scrollToBottom();
  }

  private scrollToBottom(): void {
    setTimeout(() => {
      if (this.messageContainer?.nativeElement) {
        const container = this.messageContainer.nativeElement;
        container.scrollTo({ top: container.scrollHeight, behavior: 'auto' });
      }
    }, 0);
  }
  toggle(value: boolean) {
    value = !value;
  }
  toggleMenu() {
    this.isSideBarActive = !this.isSideBarActive;
  }
  toggleGroupsAccordion() {
    this.isGroupsAccordionOpen = !this.isGroupsAccordionOpen;
  }

  toggleFriendsAccordion() {
    this.isFriendsAccordionOpen = !this.isFriendsAccordionOpen;
  }

  loadHistory(contactId: string, type: string): void {
    this.currentContact = contactId;
    this.contactType = type;

    if (this.currentContact && this.user!.id) {
      (this.contactType === 'group')
        ? this.chatService.getGroupMessageHistory(this.currentContact, this.user!.id)
        : this.chatService.getMessageHistory(this.user!.id, this.currentContact);
    }
  }
  sendMessage() {
    if (this.user?.id !== null && this.currentContact && this.messageContent) {
      (this.contactType === 'group')
        ? this.chatService.sendGroupMessage(this.currentContact, this.user!.id, this.messageContent)
        : this.chatService.sendMessageToUser(this.user!.id, this.currentContact,  this.messageContent)
      this.messageContent = '';
    }
  }

}