import {} from '@material-ui/core';

import { Incident, ListParams } from 'models';
import { call, debounce, put, takeLatest } from 'redux-saga/effects';
import { ListResponse } from '../../models/common';
import { PayloadAction } from '@reduxjs/toolkit';
import { incidentActions } from './incidentSlice';
import incidentApi from 'api/incidentApi';

function* fetchIncidentList(action: PayloadAction<ListParams>) {
  try {
    const response: ListResponse<Incident> = yield call(
      incidentApi.getAll,
      action.payload
    );
    yield put(incidentActions.fetchIncidentListSuccess(response));
  } catch (error) {
    if (error instanceof Error) { 
      yield put(incidentActions.fetchIncidentListFailed(error.message));
    }
  }
}


function* handleSearchDebounce(action: PayloadAction<ListParams>) {
  yield put(incidentActions.setFilter(action.payload));
}

export default function* incidentSaga() {
  yield takeLatest(incidentActions.fetchIncidentList.type, fetchIncidentList);
  
  yield debounce(
    500,
    incidentActions.setFilterWithDebounce.type,
    handleSearchDebounce
  );
}
