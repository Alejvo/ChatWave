import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  public toasts:{message:string;type:string}[]=[];

  show(message:string,type: 'Success'|'Error'|'Info' = 'Info'){
    this.toasts.push({message,type});

    setTimeout(()=>this.toasts.shift(), 3000);
  }
}
