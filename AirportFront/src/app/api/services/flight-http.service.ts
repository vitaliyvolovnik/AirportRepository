import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Flight } from "../models/Flight";
import { FlightState } from "../models/enums/FlightState";
import { API_URL } from "src/app/config/constant";


@Injectable({providedIn:"root"})
export class FlightHttpService{

    private readonly FLIGHT_URL = `${API_URL}/Flight`;

    constructor(private httpClient:HttpClient){

    }


    create(flight:Flight){
        return this.httpClient.post(`${this.FLIGHT_URL}`,flight)
    }

    update(flight:Flight,id:number){

    }

    get(id:number){

    }

    changeStatus(id: number, status: string){
        return this.httpClient.patch(`${this.FLIGHT_URL}/${id}/${status}`,null)
    }

    getPaged(page: number, pageSize:number){
        return this.httpClient.get(`${this.FLIGHT_URL}/${page}/${pageSize}`)
    }

    getPagedByState(page: number, pageSize:number,state:FlightState){
        
    }

    getByCrewId(id:number){
        
    }

    getPagedCrewId(page: number, pageSize: number, state: FlightState, id:number){

    }
}