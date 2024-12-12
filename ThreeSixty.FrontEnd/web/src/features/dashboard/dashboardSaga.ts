import { Entity } from 'models';
import { dashboardActions } from './dashboardSlice';
import { all, call, put, takeLatest } from 'redux-saga/effects';
import { ListResponse } from './../../models/common';
import entityApi from 'api/entityApi';


function* fetchStatistics() {
  // Run at the same time, if all Effect Creators inside all are blocking then all will be blocking and vice versa
  const responseList: Array<ListResponse<Entity>> = yield all([
    // Blocking
    call(entityApi.getDashboard, {}), // 
    call(entityApi.getDashboard, {}), //
    call(entityApi.getDashboard, {}), //
    call(entityApi.getDashboard, {}), //
  ]);

  const statisticsList = responseList.map((x) => x.pagination._totalRows);
  const [numberOfIncidentToday, numberOfIncidentThisWeek, numberOfIncidentThisMonth, totalNumberOfIncidentsAllTime] = statisticsList;
  yield put(
    dashboardActions.setStatistics({
      numberOfIncidentToday,
      numberOfIncidentThisWeek,
      numberOfIncidentThisMonth,
      totalNumberOfIncidentsAllTime,
    })
  );
}

function* fetchHighestEntityList() {
  const { data }: ListResponse<any> = yield call(entityApi.getDashboard, {});



  yield put(dashboardActions.setHighestEntityList(data));
}

function* fetchDashboardData() {
  try {
    yield all([
      call(fetchStatistics),
      call(fetchHighestEntityList),
    ]);

    yield put(dashboardActions.fetchDataSuccess());
  } catch (error) {
    console.log(`Failed to fetch dashboard data`, error);
    yield put(dashboardActions.fetchDataFailed());
  }
}

export default function* dashboardSaga() {
  yield takeLatest(dashboardActions.fetchData.type, fetchDashboardData);
}
