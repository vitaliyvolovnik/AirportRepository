import { PlaneState } from "./enums/PlaneState";

export interface Plane{
    id?:number;
    manufacturer?:string;
    maxCargoWeight?:number;
    model?:string;
    capacity?:number;
    state?:PlaneState;

}