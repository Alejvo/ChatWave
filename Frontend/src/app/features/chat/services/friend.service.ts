import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
import { friend } from 'src/app/core/models/friend';
import { user } from 'src/app/core/models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  private apiUrl = environment.apiUrl;
    
  constructor(private http: HttpClient) { }

  getRequests(userId:string):Observable<friend[]>{
    const params = new HttpParams().set('userId',userId)
    return this.http.get<friend[]>(`${this.apiUrl}/api/friends/request`, {params})
    .pipe(
      map((response:any) => response.value)
    );
  }

  makeFriendRequest(senderId: string, receiverId: string): Observable<HttpResponse<any>> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const params = { senderId, receiverId }
    return this.http.post<any>(`${this.apiUrl}/api/friends/request`, params, { headers, observe: 'response' })
  }

  addFriend(senderId: string, receiverId: string): Observable<HttpResponse<any>> {
      const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const params = { senderId, receiverId }
      return this.http.post<any>(`${this.apiUrl}/api/friends/add`, params, { headers, observe: 'response' })
    }

  removeFriend(senderId: string, receiverId: string){

  }
}
