import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { user } from '../models/user';
import { map, Observable, tap } from 'rxjs';
import { PagedList } from '../models/pagedList';
import { friend } from '../models/friend';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;
  private appUser: user | null = null;

  constructor(private http: HttpClient) { }
  
  setUser(user: any): void {
    this.appUser = user; // Actualiza el valor en el BehaviorSubject
  }

  getUser(): user | null {
    return this.appUser; 
  }

  getUserById(id: string): Observable<user> {
    let params = new HttpParams().set('id', id);
    return this.http.get(`${this.apiUrl}/api/users/id/${id}`, { params }).pipe(
      map((response:any) => response.value)
    )

  }

  getUsers(
    page:number,
    pageSize:number,
    currentUserId:string,
    searchTerm?:string,
    sortColumn?:string,
    sortOrder?:string
  ): Observable<PagedList<user>> {
    let params:HttpParams = new HttpParams()
      .set('page',page.toString())
      .set('pageSize',pageSize.toString())
      .set('currentUserId', currentUserId);

    if (searchTerm) params.set('searchTerm', searchTerm);
    if (sortColumn) params.set('sortColumn', sortColumn);
    if (sortOrder) params.set('sortOrder', sortOrder);

    return this.http.get(`${this.apiUrl}/api/users`,{ params })
    .pipe(map((response: any) => response.value));
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
    return this.http.post<any>(`${this.apiUrl}/api/users`, params, { headers });
  }

}
