import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MessageService } from 'primeng/api';
import { first } from 'rxjs';
import { AuthHttpService } from 'src/app/api/services/auth-http.service';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.scss']
})
export class ConfirmComponent implements OnInit {

  constructor(private authService:AuthHttpService,
    private messegeService:MessageService,
    private router:Router,
    private activatedRoute: ActivatedRoute ) {
      var token = activatedRoute.snapshot.params["token"]
      console.log(token)
      this.authService.confirm(token)
      .pipe(first())
      .subscribe({
        next: () =>{
          this.messegeService.add({severity:'success', summary: 'Confirm email', detail: 'Email seccesfuly confirmed'})
          this.router.navigate(["/"])
        },
        error:()=>{
          this.messegeService.add({severity:'error', summary: 'Confirm email', detail: 'token expiry or not exist'})
          this.router.navigate(["/"])
        }
      })
      
     }

  ngOnInit(): void {
  }

}


@NgModule({
  declarations: [
    ConfirmComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{path: "", component: ConfirmComponent}])
  ]
})
export class ConfirmModule { }
