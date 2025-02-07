import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl;
  private tokenKey = 'token';
  private authStatus = new BehaviorSubject<boolean>(this.hasValidToken());
  authStatus$ = this.authStatus.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  private decodeToken(token: string): any {
    try {
      return jwtDecode(token);
    } catch (error) {
      console.error('Token error', error);
      return null;
    }
  }

  private hasValidToken(): boolean {
    const token = this.getToken();
    if (!token) return false;
    const decodedToken = this.decodeToken(token);
    if (!decodedToken || decodedToken.exp * 1000 <= Date.now()) {
      this.logout();
      return false;
    }
    return true;
  }

  getUserInfoFromToken(): { userId: string, username: string } | null {
    const token = this.getToken();
    if (!token) return null;
    const decodedToken = this.decodeToken(token);
    return decodedToken ? { userId: decodedToken.sub, username: decodedToken.unique_name } : null;
  }

  login(email: string, password: string): Observable<boolean> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(`${this.apiUrl}/api/users/login`, { email, password }, { headers, observe: 'response' })
      .pipe(
        map(response => {
          if (response.status === 200 && response.body.token) {
            localStorage.setItem(this.tokenKey, response.body.token);
            this.authStatus.next(true);
            return true;
          }
          return false;
        }),
        catchError(error => {
          console.error('Login Failed', error);
          return of(false);
        })
      );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.authStatus.next(false);
    this.router.navigate(['/home']);
  }

  isLoggedIn(): boolean {
    return this.authStatus.value;
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}
