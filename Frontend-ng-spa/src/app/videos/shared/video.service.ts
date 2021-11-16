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
    return this._http.get<VideoDto[]>('http://localhost:5000/api/Videos');
  }
}
