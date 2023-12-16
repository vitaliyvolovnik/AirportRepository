import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Flight } from "../models/Flight";
import { FlightState } from "../models/enums/FlightState";


@Injectable({providedIn:"root"})
export class FlightHttpService{

    constructor(private httpClient:HttpClient){

    }


    create(flight:Flight){

    }

    update(flight:Flight,id:number){

    }

    get(id:number){

    }

    getPaged(page: number, pageSize:number){

    }

    getPagedByState(page: number, pageSize:number,state:FlightState){
        
    }

    getByCrewId(id:number){
        
    }

    getPagedCrewId(page: number, pageSize: number, state: FlightState, id:number){

    }
}