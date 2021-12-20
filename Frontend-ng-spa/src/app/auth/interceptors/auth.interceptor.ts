import { AuthService } from './../shared/auth.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private _auth:AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token=this._auth.getToken();
    if(token){
      request=request.clone(
        {
          headers:request.headers.set('Authorization','Bearer '+token)
        });
    }
    return next.handle(request).pipe(
      catchError(err=>{
        if(err.status==401)
       {
        console.error(err.error?err.error:err.message);
       }
        return throwError(err);
      })
    ) ;
  }
}
