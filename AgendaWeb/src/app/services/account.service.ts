import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { SignInRequest, SignUpRequest } from '../dtos/requests';
import { Observable } from 'rxjs';
import { Route, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(
    private data: DataService,
    private httpClient: HttpClient,
    private router: Router
  ) {
  }

  signin({ email, password }: SignInRequest): Observable<object> {
    return this.httpClient.post(`${this.data.baseURL}/signin`, { email, password }, { headers: this.data.composeHeaders() })
  }

  signup(params: SignUpRequest): Observable<object> {
    return this.httpClient.post(`${this.data.baseURL}/signup`, { ...params }, { headers: this.data.composeHeaders() })
  }

  logout() {
    localStorage.clear()
    this.router.navigateByUrl('/signin')
  }
}
