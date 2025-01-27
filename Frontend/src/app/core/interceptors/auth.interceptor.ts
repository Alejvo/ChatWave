import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { AuthService } from 'src/app/features/auth/services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.authService.getToken(); // Asegúrate de que este método devuelva string | null

    let authReq = request;

    if (token && !request.headers.has('Authorization')) {
      authReq = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(authReq).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('No Authorized. Redirecting to login.');
          this.authService.logout(); // O cualquier lógica de manejo
        }
        throw error;
      })
    );
  }
}