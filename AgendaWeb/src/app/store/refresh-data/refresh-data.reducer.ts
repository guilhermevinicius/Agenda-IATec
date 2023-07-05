import { createReducer, on } from "@ngrx/store";
import { refreshData, refreshDataReset } from "./refresh-data.action";

export const refreshDataReducer = createReducer(
  'false',
  on(refreshData, (state) => 'true'),
  on(refreshDataReset, (state) => 'false')
)
