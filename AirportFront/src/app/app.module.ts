import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {ToastModule} from "primeng/toast";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";


let routes:Routes = [
  {path: "", loadChildren: ()=> import("./modules/main/main.module").then(x => x.MainModule)},
  {path: "workspace", loadChildren: ()=> import("./modules/workspace/workspace.module").then(x => x.WorkspaceModule)}
]



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    ToastModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    RouterModule.forChild(routes),
    BrowserAnimationsModule,
  ],
  providers: [MessageService,
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
