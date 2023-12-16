import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { debounce, debounceTime, finalize, first, last } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { RestPage } from 'src/app/api/models/RestPage';
import { Pagination } from 'src/app/api/models/Pagination';
import { TagModule } from 'primeng/tag';
import { RouterModule } from '@angular/router';
import { MessageService } from 'primeng/api';
import { User } from 'src/app/api/models/User';
import { AdminHttpService } from 'src/app/api/services/admin-http.service';
import { Employee } from 'src/app/api/models/Employee';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';




@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit {

  employees:RestPage<Employee> = new RestPage<Employee>()
  lastPag!:Pagination;
  isLoading:boolean = false
  selectedEmployee?:Employee;
  employeeFormGrou!:FormGroup;
  posts:any[] =[
    {name:"Pilot"},
    {name:"Manager"},
    {name:"Flight Attendant"},
    {name:"Aviation Engineer"},
    {name:"Baggage Handler"},
    {name:"Airport Security Officer"},
    {name:"Security Screener"},
    {name:"Airport Director"},
  ]

  constructor(private messageService:MessageService,
    private adminService:AdminHttpService,
    private formBuilder:FormBuilder) { }

  ngOnInit(): void {
    this.crateFormGroup()
  }

  onLazyLoad(event:any){
    this.lastPag = Pagination.fromPrimeNg(event)
    this.loadData(Pagination.fromPrimeNg(event))
  }

  loadData(pagination:Pagination = new Pagination()){
    this.isLoading = true

    this.adminService.getPaged(pagination.page,pagination.size)
    .pipe(first(), finalize(() => this.isLoading = false))
    .subscribe({
      next: (employees:RestPage<Employee>) => {
        this.employees = employees
      },
      error: (err)=>{
        console.log(err)
        this.messageService.add({ severity: 'error', summary: 'Cannot load data', detail:"err" })
      }
    })
  
  
  }
  update(){
    this.loadData(this.lastPag)
  }

  dismit(employeeId:number){
    console.log(1);
    this.adminService.dismit(employeeId)
    .pipe(first())
    .subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Employee dismit', detail: 'Employee seccessfuly dismited' })
        this.update()
      },
      error: () => this.messageService.add({ severity: 'error', summary: 'Employee dismit', detail:"cannot dismit employee" })
    })
  }

  crateFormGroup(){
    let fullname = "";
    if(this.selectedEmployee?.user?.firstname != undefined ){
      fullname = this.selectedEmployee?.user?.firstname + "" 
    + this.selectedEmployee?.user?.lastname
    }
    let saluryControl = new FormControl(this.selectedEmployee?.salury)
    let postControl = new FormControl({name:this.selectedEmployee?.post})
    saluryControl.valueChanges
    .pipe(debounceTime(350))
    .subscribe({
      next: ()=>{
        console.log(1)
        let emp = this.employeeFormGrou?.value as Employee;
        emp.post = (postControl as any).name
        this.updateEmployee(this.selectedEmployee?.id ?? -1,emp)
      }
    })
    postControl.valueChanges
    .pipe(debounceTime(350))
    .subscribe({
      next: ()=>{
        console.log(1)
        let emp = this.employeeFormGrou?.value as Employee
        emp.post = (postControl.value as any).name
        console.log(emp)
        this.updateEmployee(this.selectedEmployee?.id ?? -1,emp)
      }
    })

    this.employeeFormGrou =  this.formBuilder.group({
      fullname: new FormControl({value:fullname,disabled:true}),
      email: new FormControl({value:this.selectedEmployee?.user?.email,disabled:true}),
      postobj: postControl,
      salury: saluryControl
    })
    

  }


  updateEmployee(id:number,employee:Employee){
    
    this.adminService.update(employee, id)
    .pipe(first())
    .subscribe({
      next:()=>{
        this.update()
      },
      error:(err)=>{
        console.log(err)
      }
    })
  }


  onEmployeeSelect(event:any){
    this.crateFormGroup()
  }



}





@NgModule({
  declarations: [
    EmployeesComponent
  ],
  imports: [
    ButtonModule,
    TableModule,
    TagModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    InputTextModule,
    DropdownModule,
    RouterModule.forChild([{path: "", component: EmployeesComponent}])
  ]
})
export class EmployeesModule { }
