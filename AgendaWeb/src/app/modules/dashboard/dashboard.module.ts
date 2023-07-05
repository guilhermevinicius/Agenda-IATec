import { NgModule } from "@angular/core";

import { DashboardComponent } from "./dashboard.component";
import { MaterialModule } from "src/app/material.module";
import { ComponentsModule } from "src/app/components/components.module";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@NgModule({
  imports: [CommonModule, MaterialModule, ComponentsModule, FormsModule, ReactiveFormsModule],
  declarations: [DashboardComponent]
})
export class DashboardModule { }
