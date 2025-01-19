import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  @Input() isVisible: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();
  @ViewChild('registerForm') registerForm!: NgForm;
  days: number[] = Array.from({ length: 31 }, (_, i) => i + 1)
  months: number[] = Array.from({ length: 12 }, (_, i) => i + 1)
  years: number[] = Array.from({ length: 100 }, (_, i) => 2025 - i)

  registerUser = {
    firstname: '',
    lastname: '',
    username: '',
    email: '',
    password: '',
    birthday: { day: 0, month: 0, year: 0 }
  }
  constructor(private userService: UserService) { }

  createUser() {
    const birthday = new Date(
      this.registerUser.birthday.year,
      this.registerUser.birthday.month - 1,
      this.registerUser.birthday.day)
    this.userService.registerUser(
      this.registerUser.firstname,
      this.registerUser.lastname,
      this.registerUser.email,
      this.registerUser.password,
      this.registerUser.username,
      birthday
    ).subscribe();
    this.registerForm.reset()
    this.closeModalEvent.emit();
  }
  isDateValid() {
    const day = this.registerUser.birthday.day;
    const month = this.registerUser.birthday.month;
    const year = this.registerUser.birthday.year;
    if (!day || !month || !year) {
      return false
    }
    const date = new Date(year, month - 1, day);
    return date.getFullYear() == year &&
      date.getMonth() == month - 1 &&
      date.getDate() == day;
  }
  closeModal() {
    this.closeModalEvent.emit();
  }
}
