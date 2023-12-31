import { Injectable } from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import { API_URL } from "src/app/config/constant";
import { RegisterModel } from "../models/Auth/RegisterModel";
import { Creadentials } from "../models/Auth/Creadentials";
import { RefreshTokenModel } from "../models/Auth/RefreshTokenModel";
import { SecurityService } from "src/app/services/security.service";
import { SecurityUser } from "../models/SecurityUser";
import { Observable } from 'rxjs';
import { AuthenticatedResponse } from "../models/Auth/AuthenticatedResponse";

@Injectable({providedIn: "root"})
export class AuthHttpService {
    private readonly AUTH_URL = `${API_URL}/Auth`

    constructor(private httpClient: HttpClient,
        private securityService: SecurityService) {

    }

    public register(registerModel: RegisterModel) {
        return this.httpClient.post<string>(`${this.AUTH_URL}/register`,registerModel)
    }

    public login(credentials: Creadentials):Observable<AuthenticatedResponse>{
        return this.httpClient.post<AuthenticatedResponse>(this.AUTH_URL, credentials)
    }

    public isEmailExist(email: string) {
        return this.httpClient.head(`${this.AUTH_URL}/${email}`)
    }

    public revoke() {
        return this.httpClient.delete(`${this.AUTH_URL}/revoke`,{ headers: this.getHeaders() })
    }

    public refresh(refreshModel: RefreshTokenModel) {
        return this.httpClient.post(`${this.AUTH_URL}/refresh`,refreshModel)
    }

    public confirm(token: string) {
        return this.httpClient.patch(`${this.AUTH_URL}/confirm/${token}`, null)
    }


    private getHeaders(): HttpHeaders {
        const headers = new HttpHeaders()
        headers.append('Authorization',`Bearer ${this.securityService.getJWT()}`)
        return headers
    }


}