import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Observable } from 'rxjs';
import { EventRequest } from '../dtos/requests/event-request';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  constructor(
    private data: DataService,
    private httpClient: HttpClient
  ) {
  }

  byId(eventId: string): Observable<object>{
    return this.httpClient.get(`${this.data.baseURL}/event/${eventId}`, { headers: this.data.composeHeaders() })
  }

  allEvents(): Observable<object> {
    return this.httpClient.get(`${this.data.baseURL}/events`, { headers: this.data.composeHeaders() })
  }

  allEventsShared(): Observable<object> {
    return this.httpClient.get(`${this.data.baseURL}/events/others`, { headers: this.data.composeHeaders() })
  }

  allEventsNotAccepted(): Observable<object> {
    return this.httpClient.get(`${this.data.baseURL}/events/not/accepted`, { headers: this.data.composeHeaders() })
  }

  search(q: string, date: string) {
    return this.httpClient.get(`${this.data.baseURL}/event/search?q=${q}&date=${date}`, { headers: this.data.composeHeaders() })
  }

  create(event: EventRequest) {
    return this.httpClient.post(`${this.data.baseURL}/event`, { ...event }, { headers: this.data.composeHeaders() })
  }

  update(eventId: string, event: EventRequest) {
    return this.httpClient.put(`${this.data.baseURL}/event/${eventId}`, { ...event }, { headers: this.data.composeHeaders() })
  }

  remove(eventId: string): Observable<object> {
    return this.httpClient.delete(`${this.data.baseURL}/event/${eventId}`, { headers: this.data.composeHeaders() })
  }
}
