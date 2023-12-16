import { RouterModule, Routes } from "@angular/router";
import { WorkspaceLayoutComponent } from "./workspace-layout/workspace-layout.component";



let routes:Routes = [{
        path:"",
        component:WorkspaceLayoutComponent,
        children:[
            {path:"planes", loadChildren: () => import("./pages/planes/planes.component").then(x => x.PlanesModule)},
            {path:"users", loadChildren: () => import("./pages/users/users.component").then(x => x.UsersModule)},
            {path:"employees", loadChildren: () => import("./pages/employees/employees.component").then(x => x.EmployeesModule)},
            {path:"flights", loadChildren: () => import("./pages/flights/flights.component").then(x => x.FlightsModule)}
        ] 
    }]

export const routings = RouterModule.forChild(routes)
