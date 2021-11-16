import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VideoDto } from './video.dto';

@Injectable({
  providedIn: 'root'
})
export class VideoService {

  constructor(private _http:HttpClient) { }

  getAll():Observable<VideoDto[]>{
    return this._http.get<VideoDto[]>(environment.baseUrl+'api/Videos');
  }
}
