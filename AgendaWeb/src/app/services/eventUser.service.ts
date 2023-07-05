import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventUserService {
  constructor(
    private data: DataService,
    private httpClient: HttpClient
  ) {
  }

  create(eventId: string): Observable<object> {
    return this.httpClient.post(`${this.data.baseURL}/event/user`, { eventId }, { headers: this.data.composeHeaders() })
  }

  shared(eventId: string, userId: string): Observable<object> {
    return this.httpClient.post(`${this.data.baseURL}/event/shared`, { eventId, userId }, { headers: this.data.composeHeaders() })
  }

  accpetedEvent(eventUserId: string): Observable<object> {
    return this.httpClient.put(`${this.data.baseURL}/event/accepted/${eventUserId}`, null, { headers: this.data.composeHeaders() })
  }
}
