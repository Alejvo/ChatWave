import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { MatIconModule } from '@angular/material/icon';
import { ToastComponent } from './utilities/toast/toast.component';



@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    ToastComponent
  ],
  imports: [
    CommonModule,
    MatIconModule
  ],
  exports:[
    FooterComponent,
    HeaderComponent,
    ToastComponent
  ]
})
export class SharedModule { }
