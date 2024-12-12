import { Dashboard, PeopleAlt } from '@material-ui/icons';
import { List, ListItem, ListItemIcon, ListItemText, makeStyles } from '@material-ui/core';

import { NavLink } from 'react-router-dom';
import React from 'react';

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    height: '100%',
    maxWidth: 360,
    backgroundColor: theme.palette.background.paper,
  },

  link: {
    color: 'inherit',
    textDecoration: 'none',

    '&.active > div' : {
      backgroundColor: theme.palette.action.selected,
    }
  }
}));

export const Sidebar = () => {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <List component="nav" aria-label="main mailbox folders">
        <NavLink to="/admin/dashboard" className={classes.link}>
          <ListItem button>
            <ListItemIcon>
              <Dashboard />
            </ListItemIcon>
            <ListItemText primary="Dashboard" />
          </ListItem>
        </NavLink>
        <NavLink to="/admin/entities/getAll" className={classes.link}>
          <ListItem button>
            <ListItemIcon>
              <PeopleAlt />
            </ListItemIcon>
            <ListItemText primary="Entities" />
          </ListItem>
        </NavLink>
        <NavLink to="/admin/Incident/getAll" className={classes.link}>
          <ListItem button>
            <ListItemIcon>
              <PeopleAlt />
            </ListItemIcon>
            <ListItemText primary="Incidents" />
          </ListItem>
        </NavLink>
      </List>
    </div>
  )
}
