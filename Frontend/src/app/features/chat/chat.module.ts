import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatPageComponent } from './pages/chat-page/chat-page.component';
import { AddUserComponent } from './modals/add-user/add-user.component';
import { AddGroupComponent } from './modals/add-group/add-group.component';
import { CreateGroupComponent } from './modals/create-group/create-group.component';
import { ContactsComponent } from './components/contacts/contacts.component';
import { MessageComponent } from './components/message/message.component';
import { NotMessageComponent } from './components/not-message/not-message.component';
import { OptionsComponent } from './components/options/options.component';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ChatPageComponent,
    AddUserComponent,
    AddGroupComponent,
    CreateGroupComponent,
    ContactsComponent,
    MessageComponent,
    NotMessageComponent,
    OptionsComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    FormsModule
  ]
})
export class ChatModule { }
