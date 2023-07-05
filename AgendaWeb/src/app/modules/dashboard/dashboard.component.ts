import { formatDate } from "@angular/common";
import { Component, Inject, LOCALE_ID, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Store } from "@ngrx/store";
import { EventDTO } from "src/app/dtos/responses";
import { EventService } from "src/app/services/event.service";
import { EventUserService } from "src/app/services/eventUser.service";
import { refreshDataReset } from "src/app/store/refresh-data";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  events = []
  eventsShared: EventDTO[] = []
  eventsNotAccepted: EventDTO[] = []

  public searchForm: FormGroup;

  constructor(
    @Inject(LOCALE_ID) private locale: string,
    private service: EventService,
    private serviceEventUser: EventUserService,
    private _snackBar: MatSnackBar,
    private store: Store<{ refreshData: string }>
  ) {
    this.searchForm = new FormGroup({
      q: new FormControl(''),
      date: new FormControl('')
    })

    this.store.select('refreshData').subscribe((state: string) => {
      if (state === 'true') {
        this.loadAll()
        this.store.dispatch(refreshDataReset())
      }
    })
  }

  ngOnInit(): void {
    this.loadAll()
  }

  loadAll() {
    this.service.allEvents()
      .subscribe((res: any) => this.events = res.body)

    this.service.allEventsShared()
      .subscribe((res: any) => this.eventsShared = res.body)

    this.service.allEventsNotAccepted()
      .subscribe((res: any) => this.eventsNotAccepted = res.body)
  }

  searchEvents() {
    var q = this.searchForm.controls['q'].value
    var date = this.searchForm.controls['date'].value


    if(!q && !date) {
      this.loadAll()
    } else {
      const dateFormated = date ? formatDate(date.toISOString(), 'yyyy-MM-dd 00:00:00', this.locale): ''
      this.service.search(q, dateFormated).subscribe((res: any) => this.events = res.body)
    }
  }

  addEventMyEvents(eventId: string) {
    this.serviceEventUser.create(eventId)
      .subscribe(res => {
        this._snackBar.open('Evento adicionado')
        this.loadAll()
      }, err => this._snackBar.open(err.error.error, 'Ok'))
  }

  acceptedEvent(eventUserId: string) {
    this.serviceEventUser.accpetedEvent(eventUserId)
      .subscribe(res => {
        this._snackBar.open('Evento adicionado')
        this.loadAll()
      }, err => this._snackBar.open(err.error.error, 'Ok'))
  }
}

