import { NgModule } from "@angular/core";
import { StoreModule } from '@ngrx/store';
import { refreshDataReducer } from "./refresh-data";

@NgModule({
  imports: [
    StoreModule.forRoot({ refreshData: refreshDataReducer })
  ]
})
export class StoreAppModule { }
