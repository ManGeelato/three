import React, { useEffect } from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';

import AddEditPage from './pages/AddEditPage';
import { Box } from '@material-ui/core';
// import ListPage from './pages/ListPage';
import ListPage from './pages/ListPage';
import { incidentActions } from 'features/incident/incidentSlice';
import { useAppDispatch } from 'app/hooks';

const IncidentFeature = () => {
  const match = useRouteMatch('/');

  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(incidentActions.fetchIncidentList({}));
  }, [dispatch]);

  return (
    <Box>
      <Switch>
        {/* /admin/incidents */}
        {/* <Route path={`${match}`}> */}
        <Route path="/admin/Incident/getAll">
          <ListPage />
        </Route>

        <Route path="/admin/Incident">
          <AddEditPage />
        </Route>

        <Route path={`${match}/:incidentId`}>
          <AddEditPage />
        </Route>
      </Switch>
    </Box>
  );
};

export default IncidentFeature;
