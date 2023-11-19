import { Employee } from "./Employee";

export interface SecurityUser{
    id: number;
    email: string;
    role: string;
    employee?: Employee;

}