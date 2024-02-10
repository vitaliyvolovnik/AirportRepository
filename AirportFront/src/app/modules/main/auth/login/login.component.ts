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
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule ,FormControl, FormsModule} from '@angular/forms';
import { InputTextModule} from 'primeng/inputtext';
import { PasswordModule} from 'primeng/password';
import { ButtonModule} from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { AuthenticatedResponse } from 'src/app/api/models/Auth/AuthenticatedResponse';

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
        email: new FormControl("", [Validators.required,Validators.max(255), Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
        password: new FormControl("", Validators.required),
        remember: new FormControl(false)
      })
      
    }
  ngOnInit(): void {
  }

  login(){
    let credentials = this.formGroup.value as Creadentials
    this.authService.login(credentials)
    .pipe(first())
    .subscribe({
      next:(response: AuthenticatedResponse)=>{
        console.log(response)
        if (response.user) {
          response.user.token = response.token;
          response.user.refreshToken = response.refreshToken;
          this.securityService.login(response.user);
          this.messageService.add({ severity: 'success', summary: 'Logged In', detail: 'Successfully logged in' });
          this.router.navigate(['/']);
        }
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
    InputTextModule,
    PasswordModule,
    ButtonModule,
    CheckboxModule,
    FormsModule,
    RouterModule.forChild([{path:"", component:LoginComponent}])
  ]
})
export class LoginModule { }
