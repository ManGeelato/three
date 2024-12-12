import { all } from 'redux-saga/effects';
import { authSaga } from 'features/auth/authSaga';
import dashboardSaga from 'features/dashboard/dashboardSaga';
import entitySaga from 'features/entity/entitySaga';
import incidentSaga from 'features/incident/incidentSaga';

export default function* rootSaga() {
  yield all([ authSaga(), 
              dashboardSaga(), 
              incidentSaga(),
              entitySaga(),
            ]);
}
