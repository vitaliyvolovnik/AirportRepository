import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup,ReactiveFormsModule,FormControl, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { SecurityService } from 'src/app/services/security.service';
import { AuthHttpService } from 'src/app/api/services/auth-http.service';
import { InputTextModule} from 'primeng/inputtext';
import { PasswordModule} from 'primeng/password';
import { ButtonModule} from 'primeng/button';
import { SecurityUser } from 'src/app/api/models/SecurityUser';
import { RegisterModel } from 'src/app/api/models/Auth/RegisterModel';
import { Router, RouterModule } from '@angular/router';
import { debounceTime, first } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public formGroup!:FormGroup


  constructor(private formBuilder:FormBuilder,
    private messageService:MessageService,
    private securityService:SecurityService,
    private authService:AuthHttpService,
    private router:Router) {
      this.initForm()

      this.formGroup.controls['email'].valueChanges
      .pipe(debounceTime(350))
      .subscribe({
        next: (value) => {
          console.log(value)
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

  private initForm(){
    this.formGroup = this.formBuilder.group({
      firstname: new FormControl("", [Validators.required,Validators.maxLength(255)]),
      lastname: new FormControl("", [Validators.required,Validators.maxLength(255)]),
      email: new FormControl("", [Validators.required,Validators.maxLength(255), Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
      password: new FormControl("", [Validators.required,Validators.maxLength(255)]),
    })
  }
  register(){
    let registerModel = this.formGroup.value as RegisterModel
    this.authService.register(registerModel)
    .pipe(first())
    .subscribe({
      next:()=>{
        this.messageService.add({ severity: 'success', summary: 'Logged In', detail: 'Successfully logged in' });
        this.router.navigate(['/']);
      },
      error:(err)=>{
        console.log(err)
          this.messageService.add({severity:'warn', summary: 'Cannot log in', detail: 'Email or password entered incorectly'})
      }
    })
  }

}

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
    RouterModule.forChild([{path:"",component:RegisterComponent}])
  ]
})
export class RegisterModule { }
