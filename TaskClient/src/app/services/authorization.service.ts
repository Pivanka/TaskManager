import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { LOGIN, REGISTER } from '../models/user.models';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  baseUrl: string = "https://localhost:7199/";

  constructor(
    private http: HttpClient) { }

  login(body: LOGIN): Observable<string>{
    const requestOptions: Object = {
      responseType: 'text'
    }
    return this.http.post<string>(this.baseUrl + 'api/account/login', body, requestOptions);
  }


  register(body: REGISTER): Observable<string>{
    const requestOptions: Object = {
      responseType: 'text'
    }
    return this.http.post<string>(this.baseUrl + 'api/account/register', body, requestOptions);
  }
}
