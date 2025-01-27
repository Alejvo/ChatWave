import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { AuthService } from 'src/app/features/auth/services/auth.service';
import { AddGroupComponent } from '../../modals/add-group/add-group.component';
import { AddUserComponent } from '../../modals/add-user/add-user.component';
import { user } from 'src/app/core/models/user';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html',
  styleUrls: ['./options.component.scss']
})
export class OptionsComponent {
  @ViewChild(AddUserComponent) userModal!: AddUserComponent;
  @ViewChild(AddGroupComponent) groupModal!: AddGroupComponent;

  @Output() toggle = new EventEmitter<void>();
  isModalVisible1: boolean = false;
  isModalVisible2: boolean = false;
  isCreateGroupModalVisible: boolean = false;
  isAFriend: boolean = false;
  constructor(private authService: AuthService) { }
  closeModal1() {
    this.isModalVisible1 = !this.isModalVisible1;
  }
  closeModal2() {
    this.isModalVisible2 = !this.isModalVisible2;
  }
  toggleCreateGroupModal() {
    this.isCreateGroupModalVisible = !this.isCreateGroupModalVisible;
  }
  logOut() {
    this.authService.logout();
  }
  onClick() {
    this.toggle.emit();
  }
}
