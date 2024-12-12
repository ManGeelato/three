import {
  Box,
  Grid,
  LinearProgress,
  makeStyles,
} from '@material-ui/core';
import {
  CalendarTodayOutlined
} from '@material-ui/icons';
import React, { useEffect, useState } from 'react';
import {
  dashboardActions,
  selectDashboardLoading,
  selectHighestEntityList,
  selectLowestEntityList,
  selectRankingIncidentList,
  selectStatistics,
} from './dashboardSlice';
import { useAppDispatch, useAppSelector } from 'app/hooks';

import StatisticItem from './components/StatisticItem';
import Widget from './components/Widget';
import { Entity } from 'models';

const useStyles = makeStyles((theme) => ({
  root: {
    position: 'relative',
    paddingTop: theme.spacing(1),
  },

  loading: {
    position: 'absolute',
    top: theme.spacing(-1),
    width: '100%',
  },
}));

const Dashboard = () => {
  const classes = useStyles();
  const dispatch = useAppDispatch();
  const loading = useAppSelector(selectDashboardLoading);
  const dashboardStatistics = useAppSelector(selectHighestEntityList);

  useEffect(() => {
    dispatch(dashboardActions.fetchData());
  }, [dispatch]);

  return (
    <Box className={classes.root}>
      {/* Loading */}
      {loading && <LinearProgress className={classes.loading} />}

      {/* Statistic Section */}
      <Grid container spacing={3}>
        {
          
        }
        <Grid item xs={12} md={6} lg={3}>
          <StatisticItem
            icon={<CalendarTodayOutlined fontSize="large" color="secondary" />}
            label="Daily"
            value={dashboardStatistics.numberOfIncidentToday}
          />
        </Grid>

        <Grid item xs={12} md={6} lg={3}>
          <StatisticItem
            icon={<CalendarTodayOutlined fontSize="large" color="primary" />}
            label="Weekly"
            value={dashboardStatistics.numberOfIncidentThisWeek}
          />
        </Grid>

        <Grid item xs={12} md={6} lg={3}>
          <StatisticItem
            icon={<CalendarTodayOutlined fontSize="large" color="action" />}
            label="Monthly"
            value={dashboardStatistics.numberOfIncidentThisMonth}
          />
        </Grid>


        <Grid item xs={12} md={6} lg={3}>
          <StatisticItem
            icon={<CalendarTodayOutlined fontSize="large" color="inherit" />}
            label="All Time"
            value={dashboardStatistics.totalNumberOfIncidentsAllTime}
          />
        </Grid>
      </Grid>


    </Box>
  );
};

export default Dashboard;
