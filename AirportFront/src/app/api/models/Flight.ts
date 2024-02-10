import { Bookign } from "./Booking";
import { Employee } from "./Employee";
import { Plane } from "./Plane";
import { Terminal } from "./Terminal";

export interface Flight{
    id?:number;
    departureAddress:string;
    arrivalAddress:string;
    arrivalDate:Date;
    departureDate:Date;
    terminal:Terminal;
    ticketCost:number;
    crew?:Employee[];
    bookigs?:Bookign[];
    plane?:Plane;
    
}