import { SecurityUser } from "../SecurityUser";

export interface AuthenticatedResponse{
    user?:SecurityUser
    token:string
    refreshToken:string
}