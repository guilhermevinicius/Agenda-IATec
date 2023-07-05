import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { AccountService } from "src/app/services/account.service";
import { FormEventComponent } from "../form-event/form-event.component";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {
  constructor(
    private dialog: MatDialog,
    private service: AccountService
  ) { }

  logout() {
    this.service.logout()
  }

  openDialog() {
    this.dialog.open(FormEventComponent, {
      width: '400px',
      data: {
        eventId: null
      }
    });
  }

}
