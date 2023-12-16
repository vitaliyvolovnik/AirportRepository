import { SecurityUser } from "./SecurityUser";
import { User } from "./User";


export interface Employee{
    id?:number;
    post:string;
    salury:string;
    user?:User;
}