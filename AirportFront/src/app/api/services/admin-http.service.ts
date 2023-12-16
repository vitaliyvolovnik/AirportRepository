import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { API_URL } from "src/app/config/constant";
import { Employee } from "../models/Employee";
import { Observable } from "rxjs";
import { RestPage } from "../models/RestPage";




@Injectable({providedIn:"root"})
export class AdminHttpService{

    private readonly ADMIN_URL = `${API_URL}/Admin`;

    constructor(private httpClient:HttpClient){

    }

    public dismit(employeeId: number){
        return this.httpClient.patch(`${this.ADMIN_URL}/dismit/${employeeId}`, {})
    }  

    public promote(userId: number){
        return this.httpClient.post(`${this.ADMIN_URL}/promote/${userId}`, {})
    }  
    
    public update(employee: Employee, employeeId: number){
        return this.httpClient.put(`${this.ADMIN_URL}/${employeeId}`, employee)
    }

    public get(employeeId: number){
        return this.httpClient.get(`${this.ADMIN_URL}/${employeeId}`)
    }

    public getAll(){
        return this.httpClient.get(`${this.ADMIN_URL}`)
    }

    public getByPost(post:string){
        return this.httpClient.get(`${this.ADMIN_URL}/post/${post}`)
    }

    public getPaged(page: number, pageSize: number):Observable<RestPage<Employee>>{
        return this.httpClient.get<RestPage<Employee>>(`${this.ADMIN_URL}/${page}/${pageSize}`)
    }
}