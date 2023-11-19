import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RouterModule, Routes } from '@angular/router';

var routes:Routes = [
  { path:"", redirectTo:"login", pathMatch:'full'},
  { path:"login", loadChildren:() => import("./login/login.component").then(x=>x.LoginModule)}
]

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ]
})
export class AuthModule { }
