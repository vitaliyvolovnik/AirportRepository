import { Component, NgModule, OnInit } from '@angular/core';
import { FlightComponent } from '../flight/flight.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
  styleUrls: ['./flight-list.component.scss']
})
export class FlightListComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}



@NgModule({
  declarations: [
    FlightListComponent,
    FlightComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{path: "",component: FlightListComponent}])

  ]
})
export class FlightListModule { }
