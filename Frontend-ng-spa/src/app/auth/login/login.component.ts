import { LoginDto } from './../shared/login.dto';
import { AuthService } from './../shared/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { TokenDto } from '../shared/token.dto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm=this.fb.group({
    username:[''],
    password:['']
  });

  constructor(private fb:FormBuilder,private _authService:AuthService) { }

  ngOnInit(): void {
  }

  login(){
    const loginDto=this.loginForm.value as LoginDto;
    this._authService.login(loginDto).subscribe((token)=>{
      console.log(token);
    })
  }
}
