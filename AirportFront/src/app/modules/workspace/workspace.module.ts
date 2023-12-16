import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkspaceLayoutComponent } from './workspace-layout/workspace-layout.component';
import { WorkspaceHeaderComponent } from './workspace-header/workspace-header.component';
import { routings } from './workspace.routing';
import { TabMenuModule } from 'primeng/tabmenu';
import { BadgeModule } from 'primeng/badge';
import { PlanesComponent } from './pages/planes/planes.component';

@NgModule({
  declarations: [
    WorkspaceLayoutComponent,
    WorkspaceHeaderComponent,
  ],
  imports: [
    CommonModule,
    TabMenuModule,
    BadgeModule,
    routings
  ]
})
export class WorkspaceModule { }
