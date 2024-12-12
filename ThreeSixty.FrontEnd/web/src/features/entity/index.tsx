import React, { useEffect } from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';

import AddEditPage from './pages/AddEditPage';
import { Box } from '@material-ui/core';
import ListPage from './pages/ListPage';
import { incidentActions } from 'features/incident/incidentSlice';
import { useAppDispatch } from 'app/hooks';
import { BrowserRouter, Link } from 'react-router-dom';

const EntityFeature = () => {
  const match = useRouteMatch();

  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(incidentActions.fetchIncidentList({}));
  }, [dispatch]);

  return (
    <Box>
      <Switch>
        {/* /admin/entities */}
        {/* <Route path={`${match}`}> */}
        <Route exact path="/admin/entities/getAll">
          <ListPage />
        </Route>

        {/* <Route path={`${match.path}/add`}> */}
        <Route path="/admin/entities">
          <AddEditPage />
        </Route>

        <Route path={`${match.path}/:entityId`}>
          <AddEditPage />
        </Route>
      </Switch>
    </Box>
  );
};

export default EntityFeature;
