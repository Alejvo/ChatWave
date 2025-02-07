import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/features/auth/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit{
  IsOpened: boolean = false;
  isAuthenticated:boolean = false;
  private authSubscription!:Subscription;
  constructor(
    public authService: AuthService,
    private router: Router) {}
  
  ngOnInit(): void {
    this.authSubscription = this.authService.authStatus$.subscribe(status => {
      this.isAuthenticated = status;
    });
  }

  ngOnDestroy(): void {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }  
  checkAuthStatus() {
    this.isAuthenticated = this.authService.isLoggedIn();
    console.log('isAuthenticated:', this.isAuthenticated); // Agrega esto para depuración
  }
  toggle() {
    this.IsOpened = !this.IsOpened;
  }
  goToLoginPage(){
    this.router.navigate(['/auth/login']);
  }
  openChat() {
    this.router.navigate(['/chat']);
  }
  onLogOut() {
    this.authService.logout();
  }
}
