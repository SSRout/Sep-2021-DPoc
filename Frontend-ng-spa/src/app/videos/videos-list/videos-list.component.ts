import { VideoService } from './../shared/video.service';
import { Component, OnInit } from '@angular/core';
import { VideoDto } from '../shared/video.dto';

@Component({
  selector: 'app-videos-list',
  templateUrl: './videos-list.component.html',
  styleUrls: ['./videos-list.component.scss']
})
export class VideosListComponent implements OnInit {
  videos:VideoDto[]=[];

  constructor(private _videoService:VideoService) { }

  ngOnInit(): void {
    this._videoService.getAll().subscribe(videos=>{
      this.videos=videos;
    });
  }


}
