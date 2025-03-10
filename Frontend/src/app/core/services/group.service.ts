import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, pipe, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { group } from '../models/group';
import { PagedList } from '../models/pagedList';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  private apiUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  createGroup(
    name: string, 
    description: string,
    image : File
  ): Observable<HttpResponse<any>> {
    const formData = new FormData();
    formData.append('Name',name)
    formData.append('Description',description)
    formData.append('Image',image)

    return this.http.post<any>(`${this.apiUrl}/api/groups`, formData)
  }
  
  getGroups(
    page: number,
    pageSize: number,
    searchTerm: string,
    sortColumn: string, 
    sortOrder: string
  ): Observable<PagedList<group>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString())
      .set('searchTerm', searchTerm)
      .set('sortColumn', sortColumn)
      .set('sortOrder', sortOrder)

    return this.http.get<group[]>(`${this.apiUrl}/api/groups`,{ params })
    .pipe(map((response: any) => response.value));
  }

  getGroupsById(id: string): Observable<group[]> {
    let params = new HttpParams();
    params.set('id', id);
    return this.http.get<group[]>(`${this.apiUrl}/api/groups/id/${id}`, { params }).pipe(
      map((response: any) => response.value));
  } 

  joinGroup(groupId: string, userId: string): Observable<HttpResponse<any>> {
      const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      const params = { groupId, userId }
      return this.http.post<any>(`${this.apiUrl}/api/groups/join`, params, { headers, observe: 'response' }).pipe(
        tap(response => console.log(response.body)))
  }

  leaveGroup(){

  }
} 
