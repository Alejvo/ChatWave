import { Injectable } from '@angular/core';
import { HubConnection } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SocialService {
  public apiUrl:string = environment.apiUrl;
  private hubConnection!: HubConnection;
  
  constructor() { }
}
