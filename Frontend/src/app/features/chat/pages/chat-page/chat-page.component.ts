import { Component, ElementRef, ViewChild } from '@angular/core';
import { message } from 'src/app/core/models/message';
import { user } from 'src/app/core/models/user';
import { UserService } from 'src/app/core/services/user.service';
import { ChatService } from '../../services/chat.service';
import { AuthService } from 'src/app/features/auth/services/auth.service';

@Component({
  selector: 'app-chat-page',
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.scss']
})
export class ChatPageComponent {
  isModalVisible: boolean = false;

  user: user | null = null;
  messages!: message[];
  messageContent = "";
  currentContact = "";
  contactType !: string;
  isSideBarActive: boolean = false;
  isGroupsAccordionOpen: boolean = true;
  isFriendsAccordionOpen: boolean = true;
  isSent: boolean = true;
  userId!: string;
  username: string = '';
  photo!: string;
  imageFormat: string = 'data:image/png;base64,';
  @ViewChild('messageContainer') private messageContainer!: ElementRef;

  constructor(
    private userService: UserService,
    private chatService: ChatService,
    private authService: AuthService) { }
  ngOnInit() {
    const token = this.authService.getToken();
    if (token) {
      const info = this.authService.getUserInfoFromToken(token);
      if (info) {

        this.chatService.startConnection();
        this.chatService.messageReceived$.subscribe({
          next: (data) => {
            const status = data.senderId === this.userId ? 'Sent' : 'Received'
            this.messages.push({ ...data, status });
          }
        })
        this.chatService.messageHistory$.subscribe({
          next: (data) => {
            this.messages = data;
            this.messages = this.messages.map(msg => ({
              ...msg,
              status: msg.senderId === this.userId ? 'Sent' : 'Received'
            }));
          }
        });
        this.userId = info.userId;
        this.username = info.username;
        this.getUserById(this.userId);
      }
    } else {
      alert('error!')
    }



  }

  showModal() {
    this.isModalVisible = !this.isModalVisible;
  }
  ngAfterViewChecked() {
    this.scrollToBottom();
  }


  private scrollToBottom(): void {
    const container = this.messageContainer.nativeElement;
    container.scrollTop = container.scrollHeight;
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

  getUserById(id: string) {
    this.userService.getUserById(id).subscribe(
      {
        next: (data: user) => {
          this.user =
          {
            ...data,
            friends: data.friends.map(friend => ({
              ...friend,
              profileImage: friend.profileImage
            }))
          }
          //data;
          this.photo = `data:image/png;base64,${this.user!.profileImage}`
          console.log(this.user.friends)

        },
        error: (error) => {
          console.error('Error when tried to get user', error)
        }
      }
    );
  }
  loadHistory(contactId: string, type: string): void {
    this.currentContact = contactId;
    this.contactType = type;

    if (this.currentContact && this.userId) {
      (this.contactType === 'group')
        ? this.chatService.getGroupMessageHistory(this.currentContact, this.userId)
        : this.chatService.getMessageHistory(this.userId, this.currentContact);
    }
  }
  sendMessage() {
    if (this.userId && this.currentContact && this.messageContent) {
      (this.contactType === 'group')
        ? this.chatService.sendGroupMessage(this.currentContact, this.userId, this.messageContent)
        : this.chatService.sendMessageToUser(this.currentContact, this.userId, this.messageContent)
      this.messageContent = '';

    }
  }
}
