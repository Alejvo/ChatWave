import { Component, ElementRef, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  @ViewChild('passwordInput') input!: ElementRef;
  public email: string = '';
  public password: string = '';
  public isModalVisible = false;
  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.authService.login(this.email, this.password).subscribe({
      next: (success) => {
        if (success) {
          this.router.navigate(['/chat']);
        } else {
          alert('Login Failed');
        }
      }
    })
  }

  showModal() {
    this.isModalVisible = !this.isModalVisible;
  }
  setfocus() {
    this.input.nativeElement.focus();
  }
}
