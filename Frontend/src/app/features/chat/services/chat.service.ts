import { Injectable } from '@angular/core';
import { HttpClient, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { message } from 'src/app/core/models/message';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../auth/services/auth.service';
import { friend } from 'src/app/core/models/friend';
import { group } from 'src/app/core/models/group';
import { ToastService } from 'src/app/shared/services/toast.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  public apiUrl:string = environment.apiUrl;
  private hubConnection!: HubConnection;

  private messageReceivedSubject = new Subject<message>();
  private messageHistorySubject = new Subject<message[]>();
  private friendsSubject = new Subject<friend[]>();
  private groupsSubject = new Subject<group[]>();
  private friendRequestSubject = new Subject<friend[]>();

  messageReceived$ = this.messageReceivedSubject.asObservable();
  messageHistory$ = this.messageHistorySubject.asObservable();
  friends$ = this.friendsSubject.asObservable();
  groups$ = this.groupsSubject.asObservable();
  friendRequest$ = this.friendRequestSubject.asObservable();


  constructor(
    private authService: AuthService,
    private toastService: ToastService
  ){}

  public async startConnection(): Promise<void> { 
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/chatHub`, {
        withCredentials: true,
        accessTokenFactory: () => this.authService.getToken() || ''
      })
      .withAutomaticReconnect()
      .build();

    try {
      await this.hubConnection.start();
      this.hubConnection.on('ReceiveMessage', (message_1: any) => {
        this.messageReceivedSubject.next(message_1);
      });

      this.hubConnection.on('ReceiveMessageHistory', (messages: message[]) => {
        this.messageHistorySubject.next(messages);
      });

      this.hubConnection.on('notifyRequest', (message: string): void => {
        this.toastService.show(message, 'Success');
      });

      this.hubConnection.on('GetFriends', (friends: friend[]): void => {
        this.friendsSubject.next(friends);
      });

      this.hubConnection.on('GetGroups', (groups: group[]): void => {
        this.groupsSubject.next(groups);
      });

      this.hubConnection.on('GetFriendRequests', (friends_1: friend[]): void => {
        this.friendRequestSubject.next(friends_1);
      });
    } catch (err) {
      console.error('Error while starting connection', err);
      throw err; 
    }
  }

  public getMessageHistory(originId: String, destinyId: String) {
    this.hubConnection.invoke('GetUserMessages', originId, destinyId)
      .catch(err => console.error(err))
  }

  public sendMessageToUser(originId: string, destinyId: string, message: string) {
    this.hubConnection.invoke('SendMessageToUser', originId, destinyId ,message)
      .catch(err => console.error(err))
  }

  public getGroupMessageHistory(groupId: string, userId: string) {
    this.hubConnection.invoke('GetGroupMessages', groupId, userId)
  }

  public sendGroupMessage(groupId: string, userId: string, message: string) {
    this.hubConnection.invoke('SendMessageToGroup', groupId, userId, message)
  }

  public getFriendList(userId:string){
    this.hubConnection.invoke('GetFriendList',userId);
  }

  public getGroupList(userId:string){
    this.hubConnection.invoke('GetGroupList',userId);
  }

  public getFriendRequest(userId:string){
    this.hubConnection.invoke('GetFriendRequests',userId);
  }

  public notifyRequest(destinyId:string,username:string){
    this.hubConnection.invoke('NotifyFriendRequest',destinyId,username);
  }
}