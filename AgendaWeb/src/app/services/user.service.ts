import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(
    private data: DataService,
    private httpClient: HttpClient
  ) {
  }

  all(): Observable<object> {
    return this.httpClient.get(`${this.data.baseURL}/users`, { headers: this.data.composeHeaders() })
  }
}
