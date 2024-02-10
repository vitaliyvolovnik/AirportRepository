import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MenuItem, MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { Dropdown, DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { TabMenuModule } from 'primeng/tabmenu';
import { finalize, first } from 'rxjs';
import { Employee } from 'src/app/api/models/Employee';
import { Plane } from 'src/app/api/models/Plane';
import { Terminal } from 'src/app/api/models/Terminal';
import { AdminHttpService } from 'src/app/api/services/admin-http.service';
import { FlightHttpService } from 'src/app/api/services/flight-http.service';
import { PlaneHttpService } from 'src/app/api/services/plane-http.service';
import { TerminalHttpService } from 'src/app/api/services/terminal-http.service';
import { DragDropModule } from 'primeng/dragdrop';
import { ChipModule } from 'primeng/chip';
import { TagModule } from 'primeng/tag';
import { CardModule } from 'primeng/card';
import { Flight } from 'src/app/api/models/Flight';
import { RestPage } from 'src/app/api/models/RestPage';
import { Pagination } from 'src/app/api/models/Pagination';


@Component({
  selector: 'app-flights',
  templateUrl: './flights.component.html',
  styleUrls: ['./flights.component.scss']
})
export class FlightsComponent implements OnInit {

  items!: MenuItem[];
  activeItem!: MenuItem;

  createFormGroup!: FormGroup;
  crew: Employee[] = [];
  plane?: Plane;

  allPilots: Employee[] = [];
  allPlanes: Plane[] = [];
  allFlightAttendants: Employee[] = [];
  allTerminals: Terminal[] = [];


  toDrop:string[]=[];

  currentlyDraggingPlane: Plane | null = null;
  currentlyDraggingEmployee: Employee | null = null;

  constructor(private formBuilder:FormBuilder,
    private flightService:FlightHttpService,
    private adminService:AdminHttpService,
    private planeService:PlaneHttpService,
    private terminalService:TerminalHttpService,
    private messageService:MessageService) { }

  ngOnInit(): void {
    this.initMenu()
    this.initForm()
    this.initData()
  }

  initMenu(){
    this.items = [
      { label: 'Create', icon: 'pi pi-plus', command: ()=> {this.activeItem = this.items[0]} },
      { label: 'List', icon: 'pi pi-list', command: ()=> {this.activeItem = this.items[1]} },
      
    ];
    this.activeItem = this.items[0];
  }

  initData(){
    this.terminalService.getAll()
      .pipe(first())
      .subscribe({
        next:(terminlas)=>{
          this.allTerminals = terminlas as Terminal[]
        },
        error: (err) =>{
          console.log(err);
        }
      })

    this.planeService.getAll()
      .pipe(first())
      .subscribe({
        next:(planes) =>{
          this.allPlanes = planes as Plane[]
        },
        error: (err) => {
          console.log(err);
        }
      })

    this.adminService.getByPost("Pilot")
      .pipe(first())
      .subscribe({
        next: (pilots) => {
          this.allPilots = pilots as Employee[]
        },
        error: (err) => {
          console.log(err);
        }
      })
      this.adminService.getByPost("Flight Attendant")
      .pipe(first())
      .subscribe({
        next: (Attendants) => {
          this.allFlightAttendants = Attendants as Employee[]
        },
        error: (err) => {
          console.log(err);
        }
      })
  }

  initForm(){
    this.createFormGroup = this.formBuilder.group({
      departureAddress: new FormControl("", Validators.required),
      arrivalAddress: new FormControl("", Validators.required),
      arrivalDate: new FormControl(Date.now(), Validators.required),
      departureDate: new FormControl(Date.now(), Validators.required),
      terminal: new FormControl(null, Validators.required),
      ticketCost: new FormControl(0, Validators.required),
      plane: new FormControl({value:null,disabled:true})
    })
  }
  
  dragPlaneStart(plane: Plane) { 
    this.currentlyDraggingPlane = plane; 
  } 

  dropPlane(){ 
    if(this.currentlyDraggingPlane){
      this.plane = this.currentlyDraggingPlane;
      this.createFormGroup.controls["plane"].setValue(`${this.plane.model} ${this.plane.manufacturer} ${this.plane.capacity}`)
      
    }
  }

  dragPlaneEnd() { 
      this.currentlyDraggingPlane = null;  
  } 
  dragEmployeeStart(employee: Employee) { 
    this.currentlyDraggingEmployee = employee; 
  } 

  dragEmployeeEnd() { 
      this.currentlyDraggingEmployee = null;  
  } 

  dropEmployee(){
    if(this.currentlyDraggingEmployee){
      this.crew.push(this.currentlyDraggingEmployee)
      console.log(this.crew)
    }
  }

  create(){
    let flight = this.createFormGroup.value as Flight
    flight.plane = this.plane
    flight.crew = this.crew
    
    this.flightService.create(flight)
    .pipe(first())
    .subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Add flight', detail: 'flight is added seccesfuly' })
        this.initForm()
        this.plane = undefined;
        this.crew = [];

      },
      error: (err) =>{
          this.messageService.add({ severity:"error", summary: 'Add flight', detail: 'Cannot add flight'})
      }
    })
  }

  flights: RestPage<Flight> = new RestPage<Flight>();
  lastPag!: Pagination;
  
  isLoading: boolean = false;

  onLazyLoad(event: any) {

    this.lastPag = Pagination.fromPrimeNg(event);
    this.loadData(Pagination.fromPrimeNg(event));
  }

  loadData(pagination: Pagination = new Pagination()) {
    this.isLoading = true;
    this.flightService.getPaged(pagination.page, pagination.size)
      .pipe(first(), finalize(() => this.isLoading = false))
      .subscribe({
        next: (flights) => {
          console.log(flights)
          this.flights = flights as RestPage<Flight>;
        },
        error: error => console.error(error)
      })
  }
//<td></td>
  cansel(id:number){
    this.flightService.changeStatus(id,"CANSELED")
    .pipe(first())
    .subscribe({
      next:()=>{
        this.loadData(this.lastPag);
      }
    })
  }
  restore(id:number){
    this.flightService.changeStatus(id,"EXPECTED")
    .pipe(first())
    .subscribe({
      next:()=>{
        this.loadData(this.lastPag);
      }
    })
  }

  getSevereti(status:any){
    switch(status){
      case "EXPECTED":
        return 'success';
        case "POSTPONED":
        return 'warning';
        case "FINISHED":
        return 'info';
        case "CANSELED":
        return 'danger';

      
    }
    return ""
  }

}


@NgModule({
  declarations: [
    FlightsComponent
  ],
  imports: [
    CommonModule,
    TabMenuModule,
    ReactiveFormsModule,
    FormsModule,
    TableModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    CalendarModule,
    DragDropModule,
    ChipModule,
    TagModule,
    CardModule,
    RouterModule.forChild([{path: "", component: FlightsComponent}])
  ]
})
export class FlightsModule { }
