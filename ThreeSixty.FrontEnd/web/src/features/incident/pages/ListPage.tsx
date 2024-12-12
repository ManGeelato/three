import {
  Box,
  Button,
  LinearProgress,
  Typography,
  makeStyles,
} from '@material-ui/core';
import { Incident, ListParams } from 'models';
import { Link, useHistory, useRouteMatch } from 'react-router-dom';
import React, { useEffect } from 'react';
import {
  incidentActions,
  selectIncidentFilter,
  selectIncidentLoading,
  selectIncidentPagination,
} from '../incidentSlice';
import { selectIncidentList, selectIncidentMap } from 'features/incident/incidentSlice';
import { useAppDispatch, useAppSelector } from 'app/hooks';
import IncidentFilters from '../components/IncidentFilters';
import IncidentTable from '../components/IncidentTable';
import { Pagination } from '@material-ui/lab';
import incidentApi from 'api/incidentApi';
import { toast } from 'react-toastify';

const useStyles = makeStyles((theme) => ({
  root: {
    position: 'relative',
    paddingTop: theme.spacing(1),
  },

  titleContainer: {
    display: 'flex',
    flexFlow: 'row nowrap',
    justifyContent: 'space-between',
    alignItems: 'center',

    marginBottom: theme.spacing(4),
  },

  loading: {
    position: 'absolute',
    top: theme.spacing(-1),
    width: '100%',
  },
}));

const ListPage = () => {
  const classes = useStyles();
  const match = useRouteMatch('/');
  const history = useHistory();

  const dispatch = useAppDispatch();

  const incidentList = useAppSelector(selectIncidentList);
  const pagination = useAppSelector(selectIncidentPagination);
  const filter = useAppSelector(selectIncidentFilter);
  const incidentLoading = useAppSelector(selectIncidentLoading);
  const incidentMap = useAppSelector(selectIncidentMap);

  useEffect(() => {
    dispatch(incidentActions.fetchIncidentList(filter));
  }, [dispatch, filter]);

  const handlePageChange = (e: any, page: number) => {
    dispatch(
      incidentActions.setFilter({
        ...filter,
        _page: page,
      })
    );
  };

  const handleSearchChange = (newFilter: ListParams) => {
    dispatch(incidentActions.setFilterWithDebounce(newFilter));
  };

  const handleFilterChange = (newFilter: ListParams) => {
    dispatch(incidentActions.setFilter(newFilter));
  };

  const handleRemoveIncident = (incident: Incident) => {
    try {
      // Remove incident API
      incidentApi.deleteIncident(incident?.id || 0);

      toast.success('Remove incident successfully!');

      // Trigger to re-fetch incident list with current filter
      const newFilter = { ...filter };
      dispatch(incidentActions.fetchIncidentList(newFilter));
    } catch (error) {
      toast.error('Remove incident failed!');
    }
  };

  const handleEditIncident = async (incident: Incident) => {
    history.push(`${match}/${incident.id}`);
  };

  return (
    <Box className={classes.root}>
      {(incidentLoading || incidentLoading) && (
        <LinearProgress className={classes.loading} />
      )}

      <Box className={classes.titleContainer}>
        <Typography variant="h5">Incidents</Typography>

        {/* <Link to={`${match}/add`} style={{ textDecoration: 'none' }}>
          <Button variant="contained" color="primary">
            Add new incident
          </Button>
        </Link> */}
      </Box>

      <Box mb={3}>
        {/* Filter */}
        <IncidentFilters
          filter={filter}
          incidentList={incidentList}
          onSearchChange={handleSearchChange}
          onChange={handleFilterChange}
        />
      </Box>

      {/* Incident Table */}
      <IncidentTable
        incidentList={incidentList}
        incidentMap={incidentMap}
        pageSize={pagination._limit}
        currentPage={pagination._page}
        onEdit={handleEditIncident}
        onRemove={handleRemoveIncident} 
        filter={filter} />

      {/* Pagination */}
      <Box my={2} display="flex" justifyContent="flex-end">
        <Pagination
          color="primary"
          count={Math.ceil(pagination._totalRows / pagination._limit)}
          page={pagination._page}
          onChange={handlePageChange}
        />
      </Box>
    </Box>
  );
};

export default ListPage;
