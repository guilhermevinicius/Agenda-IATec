import { Component, Inject } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { UserModel } from "src/app/dtos/models";
import { SharedEventModel } from "src/app/dtos/models/shared-event-model";
import { EventUserService } from "src/app/services/eventUser.service";
import { UserService } from "src/app/services/user.service";

@Component({
  selector: 'app-shared-event',
  templateUrl: './shared-event.component.html'
})
export class SharedEventComponent {
  public users: UserModel[] = []
  public sharedEventForm: FormGroup

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: SharedEventModel,
    private service: UserService,
    private eventUserSerice: EventUserService,
    private _snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<SharedEventComponent>
  ) {
    this.sharedEventForm = new FormGroup({
      userId: new FormControl('', [Validators.required])
    })
    this.allUsers()
  }

  allUsers() {
    this.service.all().subscribe((res: any) => this.users = res.body, err => console.log(err))
  }

  submitForm() {
    const userId = this.sharedEventForm.controls['userId'].value
    this.eventUserSerice.shared(this.data.eventId, userId)
      .subscribe(res => {
        this._snackBar.open('Compartilhado', 'Ok')
        this.dialogRef.close()
      }, err => this._snackBar.open(err.error.error, 'Ok'))
  }

}
