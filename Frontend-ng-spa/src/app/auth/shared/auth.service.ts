import { environment } from './../../../environments/environment';
import { LoginDto } from './login.dto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { TokenDto } from './token.dto';
import { tap,take } from 'rxjs/operators';
import { of } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLogedIn$=new BehaviorSubject<string|null>(this.getToken());
  constructor(private _http:HttpClient) { }

  login(loginDto:LoginDto):Observable<TokenDto>{
    return this._http.post<TokenDto>(environment.baseUrl+'api/Auth/Login',loginDto)
    .pipe(
      tap(token=>{
        if(token && token.jwt){
          localStorage.setItem('jwtToken',token.jwt);
          this.isLogedIn$.next(token.jwt)
        }
         else{
           this.logout();
         } 
      })
    );
  }

  getToken():string|null{
    return localStorage.getItem('jwtToken');
  }

  logout():Observable<boolean>{
    localStorage.removeItem('jwtToken');
    this.isLogedIn$.next(null);
    return of(true).pipe(take(1));
  }
}
