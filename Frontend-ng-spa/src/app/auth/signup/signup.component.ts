import { Router } from '@angular/router';
import { AuthService } from './../shared/auth.service';
import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { SignupDto } from '../shared/signup.dto';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent {
  public userForm = this._fb.group({
    userName: [''],
    password: [''],
  });
  public err: string | undefined;
  public message: string | undefined;
  constructor(
    private _fb: FormBuilder,
    private _auth: AuthService,
    private _router: Router
  ) {}

  public create() {
    const userDto=this.userForm.value as SignupDto
    this._auth.CreateUser(userDto).pipe(
      catchError(err=>{
        this.err=err.error?err.error:err.message;
        this.message=undefined;
        return throwError(err);
      })
    ).subscribe(()=>{
      this.userForm.reset();
      this.err=undefined;
      this.message="New User Created"
    });


  }
}
