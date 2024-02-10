import { Component, OnInit } from '@angular/core';
import { SecurityService } from 'src/app/services/security.service';


@Component({
  selector: 'app-workspace-header',
  templateUrl: './workspace-header.component.html',
  styleUrls: ['./workspace-header.component.scss']
})
export class WorkspaceHeaderComponent implements OnInit {

  constructor(public securityService:SecurityService) { }

  ngOnInit(): void {
  }

  logout(){
    this.securityService.logout()
  }
}
