import { RouterModule, Routes } from "@angular/router";
import { MainLayoutComponent } from "./main-layout/main-layout.component";
import { NotAuthenticatedGuard } from "src/app/guards/not-authenticated.guard";




let routes:Routes = [
    {   
        path: "", 
        component: MainLayoutComponent,
        children: [
            {path:"auth", loadChildren:()=> import("./auth/auth.module").then(x=>x.AuthModule), canActivate:[NotAuthenticatedGuard]},
            {path:"flights", loadChildren:()=> import("./Flights/flight-list/flight-list.component").then(x => x.FlightListModule)}
        ]
    }
]

export const routing = RouterModule.forChild(routes)