import { Injectable } from "@angular/core";
import { SecurityUser } from "../api/models/SecurityUser";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";


@Injectable({providedIn:"root"})
export class SecurityService{
    private readonly USER_KEY = "SECURITY_USER"

    constructor(private router:Router){

    }

    private _isAuthenticated$:BehaviorSubject<boolean>  = new BehaviorSubject<boolean>(false)
    public isAuthenticated$:Observable<boolean> = this._isAuthenticated$.asObservable();

    login(user:SecurityUser){
        this.updateUserInLocalStorage(user)
        this._isAuthenticated$.next(true)
        return this.router.navigate(['/'])
    }

    logout(){
        this.removeUserFromLocalStorage()
        this._isAuthenticated$.next(false)
        return this.router.navigate(['/'])
    }


    private updateUserInLocalStorage(user: SecurityUser) {
        localStorage.setItem(this.USER_KEY, JSON.stringify(user))
    }

    isAuthenticated(): boolean {
        return localStorage.getItem(this.USER_KEY) != null
    }

    getUser(): SecurityUser {
        const stringUser: string | null = localStorage.getItem(this.USER_KEY)
        return stringUser ? JSON.parse(stringUser): null
    }

    private removeUserFromLocalStorage() {
        localStorage.removeItem(this.USER_KEY)
    }

    hasRole(role:string): boolean {
        return this.getUser()?.role === role;
    }
    
    public getJWT(){
        console.error("ADD JWT TOKEN IMPLEMENTATION")
        return ""
    }







}