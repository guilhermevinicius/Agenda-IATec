import { Component, Input } from "@angular/core";
import { FormEventComponent } from "../form-event/form-event.component";
import { MatDialog } from "@angular/material/dialog";
import { EventDTO } from "src/app/dtos/responses";
import { EventService } from "src/app/services/event.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { SharedEventComponent } from "../shared-event/shared-event.component";

@Component({
  selector: 'app-event-item',
  templateUrl: './event-item.component.html'
})
export class EventItemComponent {
  @Input() event!: EventDTO

  constructor(
    private dialog: MatDialog,
    private service: EventService,
    private _snackBar: MatSnackBar,
  ) { }

  openDialog(eventId: string) {
    this.dialog.open(FormEventComponent, {
      width: '400px',
      data: {
        eventId
      }
    });
  }

  sharedEventDialog(eventId: string) {
    this.dialog.open(SharedEventComponent, {
      width: '400px',
      data: {
        eventId
      }
    });
  }

  removeEvent(eventId: string) {
    this.service.remove(eventId).subscribe((res) => {
      this._snackBar.open('Evento removido com sucesso')
    }, (err) => this._snackBar.open(err.error.error))
  }
}
