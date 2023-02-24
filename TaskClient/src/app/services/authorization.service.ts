import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { LOGIN } from '../models/user.models';
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

}
