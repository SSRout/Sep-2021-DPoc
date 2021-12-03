import { AuthService } from './auth/shared/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  token: string|null|undefined;
  constructor(private _auth:AuthService){
    _auth.isLogedIn$.subscribe(jwt=>{
      this.token=jwt
    });
  }

  logout(){
    this._auth.logout()
  }

}
