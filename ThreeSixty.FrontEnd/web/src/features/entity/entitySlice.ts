import {
  ListParams,
  ListResponse,
  PaginationParams,
} from './../../models/common';
import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { Entity } from 'models';
import { RootState } from '../../app/store';

export interface EntityState {
  loading?: boolean;
  list: Entity[];
  filter: ListParams;
  pagination: PaginationParams;
}

const initialState: EntityState = {
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

const entitySlice = createSlice({
  name: 'entity',
  initialState,
  reducers: {
    fetchEntityList(state, action: PayloadAction<ListParams>) {
      state.loading = true;
    },
    fetchEntityListSuccess(
      state,
      action: PayloadAction<ListResponse<Entity>>
    ) {
      state.list = action.payload.data;
      state.pagination = action.payload.pagination;
      state.loading = false;
    },
    fetchEntityListFailed(state, action: PayloadAction<string>) {
      state.loading = false;
    },

    setFilter(state, action: PayloadAction<ListParams>) {
      state.filter = action.payload;
    },

    setFilterWithDebounce(state, action: PayloadAction<ListParams>) {},
  },
});

// Actions
export const entityActions = entitySlice.actions;

// Selectors
export const selectEntityLoading = (state: RootState) => state.entity.loading;
export const selectEntityList = (state: RootState) => state.entity.list;
export const selectEntityFilter = (state: RootState) => state.entity.filter;
export const selectEntityPagination = (state: RootState) => state.entity.pagination;
  
// Reducer
const entityReducer = entitySlice.reducer;
export default entityReducer;
