import {} from '@material-ui/core';

import { Entity, ListParams } from 'models';
import { call, debounce, put, takeLatest } from 'redux-saga/effects';
import { ListResponse } from './../../models/common';
import { PayloadAction } from '@reduxjs/toolkit';
import { entityActions } from './entitySlice';
import entityApi from 'api/entityApi';


function* fetchEntityList(action: PayloadAction<ListParams>) {
  try {
    const response: ListResponse<Entity> = yield call(
      entityApi.getAll,
      action.payload,
    );

    yield put(entityActions.fetchEntityListSuccess(response));
  } catch (error) {
    if (error instanceof Error) { 
      console.log('Failed to fetch entity list', error);
      yield put(entityActions.fetchEntityListFailed(error.message));
    }
  }
}

function* handleSearchDebounce(action: PayloadAction<ListParams>) {
  yield put(entityActions.setFilter(action.payload));
}

export default function* entitySaga() {
  yield takeLatest(entityActions.fetchEntityList.type, fetchEntityList);

  yield debounce(
    500,
    entityActions.setFilterWithDebounce.type,
    handleSearchDebounce
  );
}
