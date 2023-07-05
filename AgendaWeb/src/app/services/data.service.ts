import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  public baseURL = "http://localhost:5277/v1"

  constructor(private httpClient: HttpClient) { }

  public composeHeaders(): HttpHeaders {
    const token = localStorage.getItem("token")
    return new HttpHeaders().set('Authorization', `Bearer ${token}`)
  }
}
