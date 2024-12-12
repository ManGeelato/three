import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { Entity } from 'models';
import { RootState } from './../../app/store';

// export interface DashboardStatistics {
//   maleCount: number;
//   femaleCount: number;
//   highMarkCount: number;
//   lowMarkCount: number;
// }
export interface DashboardStatistics {
  numberOfIncidentToday: number;
  numberOfIncidentThisWeek: number;
  numberOfIncidentThisMonth: number;
  totalNumberOfIncidentsAllTime: number;
}

export interface RankingByIncident {
  incidentId: string;
  incidentName: string;
  rankingList: Entity[];
}

export interface DashboardState {
  loading: boolean;
  statistics: DashboardStatistics;
  dashboardStatistics: any;
  lowestEntityList: Entity[];
  rankingByIncidentList: RankingByIncident[];
}

const initialState: DashboardState = {
  loading: false,
  statistics: {
    numberOfIncidentToday: 0,
    numberOfIncidentThisWeek: 0,
    numberOfIncidentThisMonth: 0,
    totalNumberOfIncidentsAllTime: 0,
  },
  dashboardStatistics: [],
  lowestEntityList: [],
  rankingByIncidentList: [],
};

const dashboardSlice = createSlice({
  name: 'dashboard',
  initialState: initialState,
  reducers: {
    fetchData(state) {
      state.loading = true;
    },
    fetchDataSuccess(state) {
      state.loading = false;
    },
    fetchDataFailed(state) {
      state.loading = false;
    },
    
    setStatistics(state, action: PayloadAction<DashboardStatistics>) {
      state.statistics = action.payload;
    },
    setHighestEntityList(state, action: PayloadAction<any>) {
      state.dashboardStatistics = action.payload;
    },
    setLowestEntityList(state, action: PayloadAction<Entity[]>) {
      state.lowestEntityList = action.payload;
    },
    setRankingIncidentList(state, action: PayloadAction<RankingByIncident[]>) {
      state.rankingByIncidentList = action.payload;
    },
  },
});

// Actions
export const dashboardActions = dashboardSlice.actions;

// Selector
export const selectDashboardLoading = (state: RootState) => state.dashboard.loading;
export const selectStatistics = (state: RootState) => state.dashboard.statistics;
export const selectHighestEntityList = (state: RootState) => state.dashboard.dashboardStatistics;
export const selectLowestEntityList= (state: RootState) => state.dashboard.lowestEntityList;
export const selectRankingIncidentList = (state: RootState) => state.dashboard.rankingByIncidentList;

// Reducers
const dashboardReducer = dashboardSlice.reducer;
export default dashboardReducer;
