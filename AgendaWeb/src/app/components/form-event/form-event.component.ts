import { Component, Inject } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Store } from "@ngrx/store";
import { FormEventModel } from "src/app/dtos/models";
import { EventService } from "src/app/services/event.service";
import { refreshData } from "src/app/store/refresh-data/refresh-data.action";

@Component({
  selector: 'app-form-event',
  templateUrl: './form-event.component.html'
})
export class FormEventComponent {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: FormEventModel,
    private dialogRef: MatDialogRef<FormEventComponent>,
    private service: EventService,
    private _snackBar: MatSnackBar,
    private store: Store<{ refreshData: string }>
  ) {
    if (data.eventId) {
      this.getEventById(data.eventId)
    }
  }

  eventForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    date: new FormControl('', [Validators.required]),
    local: new FormControl('', [Validators.required]),
    type: new FormControl('', [Validators.required]),
  })

  getEventById(eventId: string) {
    this.service.byId(eventId).subscribe((event: any) => {
      this.eventForm.controls.name.setValue(event.body.name)
      this.eventForm.controls.description.setValue(event.body.description)
      this.eventForm.controls.date.setValue(event.body.date)
      this.eventForm.controls.local.setValue(event.body.local)
      this.eventForm.controls.type.setValue(event.body.type)
    }, err => console.log(err))
  }

  submitForm() {
    const { name, date, description, local, type } = this.eventForm.controls

    if (this.data.eventId) {
      this.service.update(this.data.eventId, {
        name: name.value!,
        date: date.value!,
        description: description.value!,
        local: local.value!,
        isShared: (Number(type.value) === 2) ? true : false
      })
        .subscribe(res => {
          this._snackBar.open('Evento atualizado', 'Ok')
          this.dialogRef.close()
        }, err => this._snackBar.open(err.error.error, 'Ok'))
    } else {
      this.service.create({
        name: name.value!,
        date: date.value!,
        description: description.value!,
        local: local.value!,
        isShared: (Number(type.value) === 2) ? true : false
      })
        .subscribe(res => {
          this._snackBar.open('Evento criado', 'Ok')
          this.dialogRef.close()
        }, err => this._snackBar.open(err.error.error, 'Ok'))
    }
    this.store.dispatch(refreshData())
    this.dialogRef.close()

  }

}
