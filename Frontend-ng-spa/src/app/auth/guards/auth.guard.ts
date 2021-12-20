import { AuthService } from './../shared/auth.service';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private _auth: AuthService,private _routr:Router) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> {
    const isValid=this.isTokenValid(this._auth.getToken());
    return isValid?of(true):this._auth.logout().pipe(
      map(() => {
        return this._routr.parseUrl('/auth/login');;
      })
    );
  }
  public isTokenValid(token: string|null) {
    if (!token || token.length <= 0) {
      return false;
    }
    const decoded=jwt_decode(token) as DecodedToken
    return Date.now()<=decoded.exp*1000;
  }
}

interface DecodedToken{
  exp:number;
}
