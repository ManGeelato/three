import { AppBar, Button, Toolbar, Typography, makeStyles } from '@material-ui/core';
import { authActions } from 'features/auth/authSlice';
import { useAppDispatch, } from 'app/hooks';
import {useHistory} from "react-router-dom";

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  title: {
    flexGrow: 1,
  },
}));

interface HeaderProps {

}

export const Header = (props: HeaderProps) => {
  const history = useHistory();
  const classes = useStyles();

  const dispatch = useAppDispatch()

  const handleLogoutClick = () => {
    // dispatch(authActions.logout())
    localStorage.removeItem("logged_in");
    history.push("/login");
  }

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" className={classes.title}>
            360 Securities
          </Typography>
          <Button color="inherit" onClick={handleLogoutClick}>Logout</Button>
        </Toolbar>
      </AppBar>
    </div>
  )
}
