import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { SecurityService } from "../services/security.service";
import { Role } from "../api/models/enums/Role";
import { Injectable } from "@angular/core";


@Injectable({providedIn:"root"})
export class EmployeeGuard implements CanActivate{

    constructor(private securityService:SecurityService,
        private router:Router){

        }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if(!this.securityService.hasRole(Role.CUSTOMER)){
            return true;
        }
        else{
            return this.router.createUrlTree(['/'])
        }
    }

}