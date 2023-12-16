import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { API_URL } from "src/app/config/constant";
import { Plane } from "../models/Plane";
import { SecurityService } from "src/app/services/security.service";
import { RestPage } from "../models/RestPage";
import { Observable } from "rxjs";




@Injectable({providedIn: "root"})
export class PlaneHttpService {

    private readonly AUTH_URL = `${API_URL}/Plane`

    constructor(private HttpClient: HttpClient,
        private securityService: SecurityService){
    }

    create(plane: Plane){
        return this.HttpClient.post(this.AUTH_URL, plane)
    }

    update(plane: Plane, id: number){
        return this.HttpClient.put(`${this.AUTH_URL}/${id}`, plane)
    }

    getAll(){
        return this.HttpClient.get(this.AUTH_URL)
    }

    get(id: number){
        return this.HttpClient.get(`${this.AUTH_URL}/${id}`)
    }

    getPaged(page: number, pageSize: number):Observable<RestPage<Plane>>{
        return this.HttpClient.get<RestPage<Plane>>(`${this.AUTH_URL}/${page}/${pageSize}`)
    }
}