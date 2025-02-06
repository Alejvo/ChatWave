import { Injectable } from '@angular/core';
import { HttpClient, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { message } from 'src/app/core/models/message';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  public apiUrl:string = environment.apiUrl;
  private hubConnection!: HubConnection;

  private messageReceivedSubject = new Subject<message>();
  private messageHistorySubject = new Subject<message[]>();

  messageReceived$ = this.messageReceivedSubject.asObservable();
  messageHistory$ = this.messageHistorySubject.asObservable();
  
  constructor(
    private authService: AuthService
  ){}

  public startConnection() {

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/chatHub`, {
        withCredentials: true,
        accessTokenFactory: () => {
          return this.authService.getToken() || '';
        }
      })
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection Started'))
      .catch(err => console.error('Error while starting connection', err))

    this.hubConnection.on('ReceiveMessage', (message: any) => {
      this.messageReceivedSubject.next(message)
    })

    this.hubConnection.on('ReceiveMessageHistory', (messages: message[]) => {
      this.messageHistorySubject.next(messages);
    })
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
}
