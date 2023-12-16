import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { finalize, first, last } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { RestPage } from 'src/app/api/models/RestPage';
import { Pagination } from 'src/app/api/models/Pagination';
import { TagModule } from 'primeng/tag';
import { RouterModule } from '@angular/router';
import { MessageService } from 'primeng/api';
import { User } from 'src/app/api/models/User';
import { UserHttpService } from 'src/app/api/services/user-http.service';
import { AdminHttpService } from 'src/app/api/services/admin-http.service';



@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  users: RestPage<User> = new RestPage<User>()
  lastPag!: Pagination;

  isLoading: boolean = false;

  constructor(private messageService: MessageService,
    private userService: UserHttpService,
    private adminService: AdminHttpService) { }

  ngOnInit(): void {
  } 

  onLazyLoad(event:any){
    this.lastPag = Pagination.fromPrimeNg(event)
    this.loadData(Pagination.fromPrimeNg(event))
  }

  loadData(pagination:Pagination = new Pagination()){
      this.isLoading = true
      
      this.userService.getPaged(pagination.page, pagination.size)
      .pipe(first(), finalize(() => this.isLoading = false))
      .subscribe({
        next: (users:RestPage<User>) => {
          this.users = users;
          console.log(this.users)
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


  promote(userId:number){
    this.adminService.promote(userId)
    .pipe(first())
    .subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'User promote', detail: 'User seccessfuly promoted' })
        this.update()
      },
      error: () => this.messageService.add({ severity: 'error', summary: 'User promote', detail:"cannot promote user" })
    })
  }

}



@NgModule({
  declarations: [
    UsersComponent
  ],
  imports: [
    ButtonModule,
    TableModule,
    TagModule,
    CommonModule,
    RouterModule.forChild([{path: "", component: UsersComponent}])
  ]
})
export class UsersModule { }
