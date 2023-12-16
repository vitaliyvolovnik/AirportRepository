import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { API_URL } from "src/app/config/constant";
import { RestPage } from "../models/RestPage";
import { User } from "../models/User";


@Injectable({providedIn: "root"})  
export class UserHttpService{

    private readonly USER_URL = `${API_URL}/User`;

    constructor(private httpClient:HttpClient){

    }

    getPaged(page: number, pageSize: number): Observable<RestPage<User>>{
        return this.httpClient.get<RestPage<User>>(`${this.USER_URL}/${page}/${pageSize}`)
    }
}