import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { message } from 'src/app/core/models/message';
import { user } from 'src/app/core/models/user';
import { UserService } from 'src/app/core/services/user.service';
import { ChatService } from '../../services/chat.service';
import { AuthService } from 'src/app/features/auth/services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-chat-page',
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.scss']
})
export class ChatPageComponent implements OnInit{
  isModalVisible: boolean = false;

  user: user | null = null;
  isLoading:boolean = true;
  user$!:Observable<user>;
  
  messages!: message[];
  messageContent = "";
  currentContact = "";
  contactType !: string;
  isSideBarActive: boolean = false;
  isGroupsAccordionOpen: boolean = true;
  isFriendsAccordionOpen: boolean = true;
  isSent: boolean = true;
  photo!: string;
  imageFormat: string = 'data:image/png;base64,';
  @ViewChild('messageContainer') private messageContainer!: ElementRef;

  constructor(
    public userService: UserService,
    private chatService: ChatService,
    private authService: AuthService) { }

  
  ngOnInit() {
    const token = this.authService.getToken();
    if (token) {
      const info = this.authService.getUserInfoFromToken(token);

      if (info) {
        this.userService.getUserById(info.userId).subscribe(
          {
            next: (data: user) => {
              this.userService.setUser(data);
              this.user = this.userService.getUser();
              this.photo = `${this.imageFormat},${this.user!.profileImage}`
              this.isLoading = false;
            },
            error: (error) => {
              console.error('Error when tried to get user', error)
            }
          }
        );
          this.chatService.startConnection();

          this.chatService.messageReceived$.subscribe({
            next: (data) => {
              const status = data.senderId === this.user?.id ? 'Sent' : 'Received'
              this.messages.push({ ...data, status });
            }
          })

          this.chatService.messageHistory$.subscribe({
            next: (data) => {
              this.messages = data;
              this.messages = this.messages.map(msg => ({
                ...msg,
                status: msg.senderId === this.user?.id ? 'Sent' : 'Received'
              }));
            }
          });
        }
        
      }
  }

  showModal() {
    this.isModalVisible = !this.isModalVisible;
  }
  ngAfterViewChecked() {
    if(!this.isLoading){
      this.scrollToBottom();
    }
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
        : this.chatService.sendMessageToUser(this.currentContact, this.user!.id, this.messageContent)
      this.messageContent = '';
    }
  }

}
