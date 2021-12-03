import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path:'videos',loadChildren:()=>
    import('./videos/videos.module').then(f=>f.VideosModule)
  },
  {
    path:'auth',loadChildren:()=>
    import('./auth/auth.module').then(f=>f.AuthModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
