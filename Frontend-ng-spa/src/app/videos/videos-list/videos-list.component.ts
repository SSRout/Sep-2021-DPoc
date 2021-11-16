import { VideoService } from './../shared/video.service';
import { Component, OnInit } from '@angular/core';
import { VideoDto } from '../shared/video.dto';
import { Observable } from 'rxjs';
import { catchError, delay } from 'rxjs/operators';

@Component({
  selector: 'app-videos-list',
  templateUrl: './videos-list.component.html',
  styleUrls: ['./videos-list.component.scss']
})
export class VideosListComponent implements OnInit {
  videos$: Observable<VideoDto[]>|undefined;
  error: any;

  constructor(private _videoService:VideoService) { }

  ngOnInit(): void {
    this.videos$=this._videoService.getAll().pipe(
      delay(1000),
      catchError(err=>{
        this.error=err;
        throw err;
      })
      );
  }


}
