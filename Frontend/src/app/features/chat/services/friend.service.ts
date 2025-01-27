import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { user } from 'src/app/core/models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  private apiUrl = environment.apiUrl;
  private appUser: user | null = null;
    
  constructor(private http: HttpClient) { }

  getRequests(userId:string){

  }

  makeFriendRequest(userId: string, friendId: string): Observable<HttpResponse<any>> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const params = { userId, friendId }
    return this.http.post<any>(`${this.apiUrl}/api/friends/request`, params, { headers, observe: 'response' })
  }

  addFriend(userId: string, friendId: string): Observable<HttpResponse<any>> {
      const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      const params = { userId, friendId }
      return this.http.post<any>(`${this.apiUrl}/api/friends/add`, params, { headers, observe: 'response' })
    }

  removeFriend(userId: string, friendId: string){

  }
}
