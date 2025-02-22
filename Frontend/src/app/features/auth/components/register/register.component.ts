import { Component, EventEmitter, Input, OnDestroy, Output, ViewChild } from '@angular/core';
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
  profileImage: File | null = null;
  days: number[] = Array.from({ length: 31 }, (_, i) => i + 1)
  months: number[] = Array.from({ length: 12 }, (_, i) => i + 1)
  years: number[] = Array.from({ length: 100 }, (_, i) => 2025 - i)
/*
  registerUser = {
    firstname: '',
    lastname: '',
    username: '',
    email: '',
    password: '',
    birthday: { day: 0, month: 0, year: 0 }
  }*/
  constructor(private userService: UserService) { }

  createUser(form:NgForm) {

    const { firstname, lastname, email, password, username, year,month,day } = form.value;
    if(!this.isDateValid(
      Number(day),
      Number(month),
      Number(year)
    )) return;

    const birthday = new Date(year,month - 1,day);

    this.userService.registerUser(
      firstname,
      lastname,
      email,
      password,
      username,
      birthday,
      this.profileImage!
    ).subscribe();
    this.registerForm.reset();
    this.closeModalEvent.emit();
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.profileImage = input.files[0];
    }
  }

  isDateValid(day:number,month:number,year:number):boolean {

    if (!day || !month || !year) return false;
    
    const date = new Date(year, month - 1, day);
    return date.getFullYear() == year &&
      date.getMonth() == month - 1 &&
      date.getDate() == day;
  }
  closeModal() {
    this.closeModalEvent.emit();
  }
  
}
