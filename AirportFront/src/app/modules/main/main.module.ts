import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { routing } from './main.routes';
import { HeaderComponent } from './header/header.component';
import { RouterOutlet } from '@angular/router';
import {ButtonModule} from "primeng/button";
import {RippleModule} from "primeng/ripple";
import { FlightComponent } from './Flights/flight/flight.component';


@NgModule({
  declarations: [
    MainLayoutComponent,
    HeaderComponent,
    FlightComponent
  ],
  imports: [
    CommonModule,
    routing,
    RouterOutlet,
    ButtonModule,
    RippleModule,
    
  ]
})
export class MainModule { }
