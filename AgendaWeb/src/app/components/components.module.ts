import { NgModule } from "@angular/core";
import { FormEventComponent } from "./form-event/form-event.component";
import { EventItemComponent } from "./event-item/event-item.component";
import { MaterialModule } from "../material.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { SharedEventComponent } from "./shared-event/shared-event.component";
import { NavbarComponent } from "./navbar/navbar.component";

@NgModule({
  declarations: [
    NavbarComponent,
    FormEventComponent,
    EventItemComponent,
    SharedEventComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    NavbarComponent,
    FormEventComponent,
    EventItemComponent
  ]
})
export class ComponentsModule {}
