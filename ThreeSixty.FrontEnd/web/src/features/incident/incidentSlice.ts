import {
  ListParams,
  ListResponse,
  PaginationParams,
} from '../../models/common';
import { PayloadAction, createSelector, createSlice } from '@reduxjs/toolkit';
import { Incident } from 'models';
import { RootState } from '../../app/store';

export interface IncidentState {
  loading?: boolean;
  list: Incident[];
  filter: ListParams;
  pagination: PaginationParams;
}

const initialState: IncidentState = {
  loading: false,
  list: [],
  filter: {
    _page: 1,
    _limit: 15,
  },
  pagination: {
    _page: 1,
    _limit: 15,
    _totalRows: 15,
  },
};

const incidentSlice = createSlice({
  name: 'incident',
  initialState,
  reducers: {
    fetchIncidentList(state, action: PayloadAction<ListParams>) {
      state.loading = true;
    },
    fetchIncidentListSuccess(
      state,
      action: PayloadAction<ListResponse<Incident>>
    ) {
      state.list = action.payload.data;
      state.pagination = action.payload.pagination;
      state.loading = false;
      // console.log("state.list", state.list);
    },
    
    fetchIncidentListFailed(state, action: PayloadAction<string>) {
      state.loading = false;
    },

    setFilter(state, action: PayloadAction<ListParams>) {
      state.filter = action.payload;
    },
    
    setFilterWithDebounce(state, action: PayloadAction<ListParams>) {},
  },
});

// Actions
export const incidentActions = incidentSlice.actions;

// Selectors
export const selectIncidentLoading = (state: RootState) => state.incident.loading;
export const selectIncidentList = (state: RootState) => state.incident.list;
export const selectIncidentFilter = (state: RootState) => state.incident.filter;
export const selectIncidentPagination = (state: RootState) => state.incident.pagination;


export const selectIncidentMap = createSelector(selectIncidentList, (incidentList) =>
  incidentList.reduce(
    (
      map: {
        [key: number]: Incident;
      },
      incident: Incident
    ) => {
      map[incident.id] = incident;
      return map;
    },
    {}
  )
);

// Reducer
const incidentReducer = incidentSlice.reducer;
export default incidentReducer;
