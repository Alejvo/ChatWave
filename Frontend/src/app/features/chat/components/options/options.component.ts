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

  modals = {
    addUser: false,
    addGroup: false,
    createGroup: false,
    friendRequest: false
  };

  isAFriend: boolean = false;

  constructor(private authService: AuthService) { }
  toggleModal(modal: keyof typeof this.modals) {
    this.modals[modal] = !this.modals[modal];
  }

  logOut() {
    this.authService.logout();
  }

  onClick() {
    this.toggle.emit();
  }
}
