import { Box, makeStyles } from '@material-ui/core';
import { Header, Sidebar } from 'components/Common';
import { lazy } from 'react';
import { Route, Switch } from 'react-router-dom';

const Dashboard = lazy(() => import('./../../features/dashboard'));
const EntityFeature = lazy(() => import('../../features/entity'));
const IncidentFeature = lazy(() => import('../../features/incident'));

const useStyles = makeStyles(theme => ({
  root: {
    display: 'grid',
    gridTemplateRows: 'auto 1fr',
    gridTemplateColumns: '240px 1fr',
    gridTemplateAreas: `"header header" "sidebar main"`,

    minHeight: '100vh',
  },

  header: {
    gridArea: "header",
    borderBottom: `1px solid ${theme.palette.divider}`
  },
  sidebar: {
    gridArea: "sidebar",
    borderRight: `1px solid ${theme.palette.divider}`
  },
  main: {
    gridArea: "main",
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(2, 3)
  }
}));

export const AdminLayout = () => {
  const classes = useStyles();

  return (
    <Box className={classes.root}>
      <Box className={classes.header}>
        <Header />
      </Box>

      <Box className={classes.sidebar}><Sidebar /></Box>
      <Box className={classes.main}>
        <Switch>
          <Route path="/admin/dashboard">
            <Dashboard />
          </Route>

          <Route path="/admin/entities">
            <EntityFeature />
          </Route> 
 
          <Route path="/admin/Incident">
            <IncidentFeature />
          </Route>

        </Switch>
      </Box>
    </Box>
  )
}