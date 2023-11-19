import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthHttpService } from 'src/app/api/services/auth-http.service';
import { SecurityService } from 'src/app/services/security.service';
import { Creadentials } from 'src/app/api/models/Auth/Creadentials';
import { MessageService } from 'primeng/api';
import { first, debounceTime } from "rxjs"
import { SecurityUser } from 'src/app/api/models/SecurityUser';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {


  formGroup:FormGroup;



  constructor(private authService:AuthHttpService,
    private securityService:SecurityService,
    private router:Router,
    private messageService:MessageService,
    private formBuilder:FormBuilder) { 
      this.formGroup = this.formBuilder.group({
        email:[{value: null}, [Validators.required, Validators.max(100)]],
        password:[{value:null}, [Validators.required, Validators.min(6),Validators.max(100)]]
      })

      this.formGroup.controls['email'].valueChanges
      .pipe(debounceTime(350))
      .subscribe({
        next: (value) => {
          this.authService.isEmailExist(value)
          .pipe(first())
          .subscribe({
            next: () => {

            },
            error: (err) => {

            }
          })
        }
      })
    }

  ngOnInit(): void {
  }

  login(){

    let credentials = this.formGroup.value as Creadentials
    this.authService.login(credentials)
    .pipe(first())
    .subscribe({
      next:(securityUser:SecurityUser)=>{
        this.securityService.login(securityUser)
        this.messageService.add({severity:'success', summary: 'Ligined', detail: 'Seccessfuly logined'})
        this.router.navigate(['/'])
      },
      error:(err)=>{
          this.messageService.add({severity:'warn', summary: 'Cannot log in', detail: 'Email or password entered incorectly'})
      }
    })
  }

}






@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([{path:"", component:LoginComponent}])
  ]
})
export class LoginModule { }
