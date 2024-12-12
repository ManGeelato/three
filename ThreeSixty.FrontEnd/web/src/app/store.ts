import { Action, ThunkAction, combineReducers, configureStore } from '@reduxjs/toolkit';
import { connectRouter, routerMiddleware } from 'connected-react-router';
import authReducer from 'features/auth/authSlice';
import createSagaMiddleware from 'redux-saga';
import dashboardReducer from 'features/dashboard/dashboardSlice';
import entityReducer from 'features/entity/entitySlice';
import { history } from 'utils';
import incidentReducer from 'features/incident/incidentSlice';
import rootSaga from './rootSaga';

const rootReducer = combineReducers({
  router: connectRouter(history),
  auth: authReducer,
  dashboard: dashboardReducer,
  entity: entityReducer,
  incident: incidentReducer,
})

const sagaMiddleware = createSagaMiddleware()

export const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(sagaMiddleware, routerMiddleware(history)),
});

sagaMiddleware.run(rootSaga)

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
