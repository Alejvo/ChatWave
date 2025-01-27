import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/features/auth/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  IsOpened: boolean = false;
  constructor(
    public authService: AuthService,
    private router: Router) { }
    
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
