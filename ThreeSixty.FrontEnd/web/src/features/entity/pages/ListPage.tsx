import {
  Box,
  Button,
  LinearProgress,
  Typography,
  makeStyles,
} from '@material-ui/core';
import { Entity, ListParams } from 'models';
import { Link, useHistory, useRouteMatch } from 'react-router-dom';
import React, { useEffect } from 'react';
import {
  entityActions,
  selectEntityFilter,
  selectEntityList,
  selectEntityLoading,
  selectEntityPagination,
} from '../entitySlice';
import { selectIncidentList, selectIncidentMap } from 'features/incident/incidentSlice';
import { useAppDispatch, useAppSelector } from 'app/hooks';

import EntityFilters from '../components/EntityFilters';
import EntityTable from '../components/EntityTable';
import { Pagination } from '@material-ui/lab';
import entityApi from 'api/entityApi';
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

  const entityList = useAppSelector(selectEntityList);
  const pagination = useAppSelector(selectEntityPagination);
  const filter = useAppSelector(selectEntityFilter);
  const entityLoading = useAppSelector(selectEntityLoading);
  const incidentMap = useAppSelector(selectIncidentMap);
  const incidentList = useAppSelector(selectIncidentList);
  const incidentLoading = useAppSelector(selectEntityLoading);

  useEffect(() => {
    dispatch(entityActions.fetchEntityList(filter));
  }, [dispatch, filter]);

  const handlePageChange = (e: any, page: number) => {
    dispatch(
      entityActions.setFilter({
        ...filter,
        _page: page,
      })
    );
  };

  const handleSearchChange = (newFilter: ListParams) => {
    dispatch(entityActions.setFilterWithDebounce(newFilter));
  };

  const handleFilterChange = (newFilter: ListParams) => {
    dispatch(entityActions.setFilter(newFilter));
  };

  const handleRemoveEntity = (entity: Entity) => {
    try {
      // Remove entity API
      entityApi.remove(entity?.id || 0);

      toast.success('Remove entity successfully!');

      // Trigger to re-fetch entity list with current filter
      const newFilter = { ...filter };
      dispatch(entityActions.fetchEntityList(newFilter));
    } catch (error) {
      toast.error('Remove entity failed!');
    }
  };

  const handleEditEntity = async (entity: Entity) => {
    history.push(`${match}/${entity.id}`);
  };

  return (
    <Box className={classes.root}>
      {(entityLoading || incidentLoading) && (
        <LinearProgress className={classes.loading} />
      )}

      <Box className={classes.titleContainer}>
        <Typography variant="h5">Entities</Typography>
      </Box>

      <Box mb={3}>
        {/* Filter */}
        <EntityFilters
          filter={filter}
          incidentList={incidentList}
          onSearchChange={handleSearchChange}
          onChange={handleFilterChange}
        />
      </Box>

      {/* Entity Table */}
      <EntityTable
        entityList={entityList}
        incidentMap={incidentMap}
        pageSize={pagination._limit}
        currentPage={pagination._page}
        onEdit={handleEditEntity}
        onRemove={handleRemoveEntity} 
        filter={filter}/>

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
