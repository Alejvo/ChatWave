import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { user } from '../models/user';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  getUsers(): Observable<user[]> {
    return this.http.get<user[]>(`${this.apiUrl}/api/users`);
  }
  getUserById(id: string): Observable<user> {
    let params = new HttpParams();
    params.set('id', id);
    return this.http.get<user>(`${this.apiUrl}/api/users/id/${id}`, { params })

  }
  getUsersByUsername(username: string): Observable<user[]> {
    let params = new HttpParams();
    params.set('username', username);
    return this.http.get<user[]>(`${this.apiUrl}/api/users/username/${username}`, { params })

  }
  registerUser(
    firstname: string,
    lastname: string,
    email: string,
    password: string,
    username: string,
    birthday: Date): Observable<HttpResponse<any>> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const params = { firstname, lastname, email, password, username, birthday }
    return this.http.post<any>(`${this.apiUrl}/api/users/register`, params, { headers });
  }

  addToGroup(groupId: string, userId: string): Observable<HttpResponse<any>> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const params = { groupId, userId }
    return this.http.post<any>(`${this.apiUrl}/api/groups/add-user`, params, { headers, observe: 'response' }).pipe(
      tap(response => console.log(response.body)))
  }
  addToFriend(userId: string, friendId: string): Observable<HttpResponse<any>> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const params = { userId, friendId }
    return this.http.post<any>(`${this.apiUrl}/api/users/add-friend`, params, { headers, observe: 'response' })
  }
}
