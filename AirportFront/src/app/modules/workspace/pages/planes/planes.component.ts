import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { PlaneState } from 'src/app/api/models/enums/PlaneState';
import { MessageService } from 'primeng/api';
import { PlaneHttpService } from 'src/app/api/services/plane-http.service';
import { Plane } from 'src/app/api/models/Plane';
import { finalize, first } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { TableModule } from 'primeng/table';
import { RestPage } from 'src/app/api/models/RestPage';
import { Pagination } from 'src/app/api/models/Pagination';
import { TagModule } from 'primeng/tag';


@Component({
  selector: 'app-planes',
  templateUrl: './planes.component.html',
  styleUrls: ['./planes.component.scss']
})
export class PlanesComponent implements OnInit {

  //create
  states:any[] =[
    {value: PlaneState.FAULTI, name:"FAULTI"},
    {value: PlaneState.SUCCESS, name:"SUCCESS"}];
  selectedState?:PlaneState;
  createFormGroup!:FormGroup;

  //list
  planes: RestPage<Plane> = new RestPage<Plane>();
  lastPag!: Pagination;
  
  isLoading: boolean = false;

  constructor(private FormBuilder: FormBuilder,
    private MessageService: MessageService,
    private PlaneService: PlaneHttpService) { }

  ngOnInit(): void {

    this.initForm()
  }

  private initForm(){
    this.createFormGroup = this.FormBuilder.group({
      manufacturer:new FormControl("", Validators.required),
      maxCargoWeigth: new FormControl(null, Validators.required),
      model: new FormControl("", Validators.required),
      capacity: new FormControl(null, Validators.required,)
    })
  }

  create(){
    if(this.createFormGroup.valid) {
        console.log(1)
        var plane = this.createFormGroup.value as Plane
        plane.state = this.selectedState
        console.log(plane)
        this.PlaneService.create(plane)
        .pipe(first())
        .subscribe({
          next: () => {
            this.MessageService.add({ severity: 'success', summary: 'Add plane', detail: 'Plane is added seccesfuly' })
            this.initForm()
          },
          error: (err) =>{
              this.MessageService.add({ severity:"error", summary: 'Add plane', detail: 'Cannot add plane'})
          }
        })
    }
  }

  setState(event:any) {
    this.selectedState = event.value.value
  }

  onLazyLoad(event: any) {

    this.lastPag = Pagination.fromPrimeNg(event);
    this.loadData(Pagination.fromPrimeNg(event));
  }

  loadData(pagination: Pagination = new Pagination()) {
    this.isLoading = true;
    this.PlaneService.getPaged(pagination.page, pagination.size)
      .pipe(first(), finalize(() => this.isLoading = false))
      .subscribe({
        next: (planes:RestPage<Plane>) => {
          this.planes = planes;
        },
        error: error => console.error(error)
      })
  }
  
  getSevereti(state:PlaneState){
    switch(state){
      case PlaneState.SUCCESS:
        return 'success';
        case PlaneState.FAULTI:
        return 'warning';
    }
  }

  getState(state:PlaneState){
    switch(state){
      case PlaneState.SUCCESS:
        return 'Success';
        case PlaneState.FAULTI:
        return 'Faulti';
    }
  }


}


@NgModule({
  declarations: [
    PlanesComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    InputTextModule,
    DropdownModule,
    ButtonModule,
    InputNumberModule,
    TableModule,
    TagModule,
    RouterModule.forChild([{path: "", component: PlanesComponent}])
  ]
})
export class PlanesModule { }
