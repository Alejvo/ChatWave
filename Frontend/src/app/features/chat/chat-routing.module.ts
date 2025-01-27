import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatPageComponent } from './pages/chat-page/chat-page.component';
import { userGuard } from 'src/app/core/guards/user.guard';

const routes: Routes = [
  { path: '', component: ChatPageComponent, canActivate: [userGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChatRoutingModule { }
