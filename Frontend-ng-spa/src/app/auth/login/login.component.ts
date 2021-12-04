import { LoginDto } from './../shared/login.dto';
import { AuthService } from './../shared/auth.service';
import { Component, OnInit,OnDestroy } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { TokenDto } from '../shared/token.dto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit,OnDestroy {

  loginForm=this._fb.group({
    username:[''],
    password:['']
  });
  //private _unsub:Subscription|undefined;
  constructor(private _fb:FormBuilder,private _authService:AuthService,private _router:Router) { }

  ngOnInit(): void {
    // this._unsub=this._authService.login(loginDto).subscribe((token)=>{
    //   console.log('token',token)
    // });
  }

  login(){
    const loginDto=this.loginForm.value as LoginDto;
    this._authService.login(loginDto).subscribe((token)=>{
      if(token && token.jwt){
        this._router.navigateByUrl('videos');
      }
    })
  }

   ngOnDestroy():void{
    // if(this._unsub){
    //   this._unsub.unsubscribe();
    // }
  }

}
