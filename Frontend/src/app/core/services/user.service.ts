import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { user } from '../models/user';
import { catchError, map, Observable, tap, throwError } from 'rxjs';
import { PagedList } from '../models/pagedList';

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
    return this.http.get<user>(`${this.apiUrl}/api/users/id/${id}`)
  }

  getUsers(
    page:number,
    pageSize:number,
    currentUserId:string,
    searchTerm:string,
    sortColumn:string,
    sortOrder:string
  ): Observable<PagedList<user>> {
    let params:HttpParams = new HttpParams()
      .set('page',page.toString())  
      .set('pageSize',pageSize.toString())
      .set('currentUserId', currentUserId)
      .set('searchTerm', searchTerm)
      .set('sortColumn', sortColumn)
      .set('sortOrder', sortOrder);

    return this.http.get(`${this.apiUrl}/api/users`,{ params })
    .pipe(
      map((response: any) => response.value)
    );
  }

  registerUser(
    firstname: string,
    lastname: string,
    email: string,
    password: string,
    username: string,
    birthday: Date,
    profileImage : File): Observable<HttpResponse<any>> {
    const formData = new FormData();
    formData.append('FirstName', firstname);
    formData.append('LastName', lastname);
    formData.append('Email', email);
    formData.append('Password', password);
    formData.append('Username', username);
    formData.append('Birthday', birthday.toISOString())
    formData.append('ProfileImage',profileImage);

    return this.http.post<any>(`${this.apiUrl}/api/users`, formData);
  }
}
