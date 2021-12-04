import { AuthService } from './auth/shared/auth.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  token: string|null|undefined;
  constructor(private _auth:AuthService,private _router:Router){
    _auth.isLogedIn$.subscribe(jwt=>{
      this.token=jwt
    });
  }

  logout(){
    this._auth.logout().pipe(
      take(1)
    ).subscribe(logout=>{
      this._router.navigateByUrl('auth/login');
    })
  }

}
