import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { filter, map } from 'rxjs';
import { MenuItemCustom } from 'src/app/models/MenuItem';

@Component({
  selector: 'app-workspace-layout',
  templateUrl: './workspace-layout.component.html',
  styleUrls: ['./workspace-layout.component.scss']
})
export class WorkspaceLayoutComponent implements OnInit {
  allItems: MenuItemCustom[]=[
    {
      path: "planes",
      postAccess: [],
      title: "Planes"
    },
    {
      path: "employees",
      postAccess: [],
      title: "Employees"
    },
    {
      path: "users",
      postAccess: [],
      title: "Users"
    },
    {
      path: "flights",
      postAccess: [],
      title: "Flights"
    }
  ]
  items: MenuItem[] =[];

  activeItem!: MenuItem;


  constructor(private router:Router,
    private route:ActivatedRoute) {
      router.events.subscribe((val) => {
        if(val instanceof NavigationEnd){
          this.ChangeActiveItem()
        }   
    });
    }

  ngOnInit() {

    this.allItems.forEach(element => {
      this.items.push({
        label: element.title,
        badge: `/workspace/${element.path}`,
        command: (event: any) => {
          this.router.navigate([`/workspace/${element.path}`])
        }
      })
    });


    this.activeItem = this.items[0]
    this.ChangeActiveItem()
 }

 private ChangeActiveItem(){
  this.router.events.pipe(
    filter(event => event instanceof NavigationEnd),
    map(() => this.route),
    map(route => {
      while (route.firstChild) {
       route = route.firstChild;
      }
      return route;
     }),
     map(route => route.url)
    )
   .subscribe({
    next: (vv: any)=> {
      let path = vv._value[0].path
      this.activeItem = this.items.find((value:MenuItem) => {
        return value.badge === path;
      }) ?? this.activeItem

    }
   })
 }
}
 