import { environment } from './../../../environments/environment';
import { LoginDto } from './login.dto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { TokenDto } from './token.dto';
import { tap,take } from 'rxjs/operators';
import { of } from 'rxjs';
import { SignupDto } from './signup.dto';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLogedIn$=new BehaviorSubject<string|null>(this.getToken());
  constructor(private _http:HttpClient) { }

  public login(loginDto:LoginDto):Observable<TokenDto>{
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

  public getToken():string|null{
    return localStorage.getItem('jwtToken');
  }

  public CreateUser(userDto:SignupDto):Observable<any>{
    return this._http.post<any>(environment.baseUrl+'api/auth/CreateUser',userDto);
  }

  public logout():Observable<boolean>{
    localStorage.removeItem('jwtToken');
    this.isLogedIn$.next(null);
    return of(true).pipe(take(1));
  }
}
