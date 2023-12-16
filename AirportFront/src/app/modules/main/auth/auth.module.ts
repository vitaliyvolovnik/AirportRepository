import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

var routes:Routes = [
  { path: "", redirectTo: "login", pathMatch: 'full'},
  { path: "login", loadChildren:() => import("./login/login.component").then(x => x.LoginModule)},
  { path: "register", loadChildren:() => import("./register/register.component").then(x => x.RegisterModule )},
  { path: "confirm/:token", loadChildren: ()=> import("./confirm/confirm.component").then(x => x.ConfirmModule)}   
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
