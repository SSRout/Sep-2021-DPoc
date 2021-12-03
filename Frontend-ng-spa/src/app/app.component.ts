import { AuthService } from './auth/shared/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private _auth:AuthService){}

  logout(){
    this._auth.logout()
  }

}
