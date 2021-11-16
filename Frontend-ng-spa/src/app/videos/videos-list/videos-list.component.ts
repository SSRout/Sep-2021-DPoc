import { VideoService } from './../shared/video.service';
import { Component, OnInit } from '@angular/core';
import { VideoDto } from '../shared/video.dto';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-videos-list',
  templateUrl: './videos-list.component.html',
  styleUrls: ['./videos-list.component.scss']
})
export class VideosListComponent implements OnInit {
  videos$: Observable<VideoDto[]>|undefined;

  constructor(private _videoService:VideoService) { }

  ngOnInit(): void {
    this.videos$=this._videoService.getAll();
  }


}
