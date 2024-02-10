import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { API_URL } from "src/app/config/constant";
import { Terminal } from "../models/Terminal";


@Injectable({providedIn:"root"})
export class TerminalHttpService{

    private readonly AUTH_URL = `${API_URL}/Flight`

    constructor(private httpClient:HttpClient){

    }


    getAll(){
        return this.httpClient.get(`${this.AUTH_URL}/terminals`);
    }



}