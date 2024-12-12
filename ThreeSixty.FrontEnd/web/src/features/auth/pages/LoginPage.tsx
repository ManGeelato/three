import {
  CssBaseline,
  Box,
  Typography,
  Grid,
  Button,
  Container,
  TextField,
  FormControlLabel,
  Checkbox,
  Link,
  AppBar,
  Toolbar,
  makeStyles
} from '@material-ui/core';
import { createTheme, ThemeProvider } from "@material-ui/core/styles";
import { useState } from 'react';
import { useHistory } from 'react-router-dom';
import { toast } from 'react-toastify';

const theme = createTheme();

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  title: {
    flexGrow: 1,
  },
}));

const LoginPage = () => {
  const history = useHistory();
  const classes = useStyles();

  const [input, setInput] = useState({
    email: '',
    password: '',
  });
  
  let isSignedIn:boolean = true

  const handleLogin = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const signedInUser = JSON.parse(localStorage.getItem('access_token') || '{}');
    if(input.email === signedInUser.email && input.password === signedInUser.password){
      toast.success(`User ${signedInUser.email} logged in successfully`);
      history.push('/admin/dashboard');
      localStorage.setItem("logged_in", String(isSignedIn));
    }else{
      toast.error(`Kindly double-check details`);
    }
  }


  return (
    <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <AppBar position="static">
            <Toolbar>
              <Typography variant="h6" className={classes.title}>
                360 Securities
              </Typography>
            </Toolbar>
          </AppBar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <Box component="form" onSubmit={handleLogin} sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoComplete="email"
              autoFocus
              value={input.email}
              onChange={(e) => setInput({...input, [e.target.name]: e.target.value}) }
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="current-password"
              value={input.password}
              onChange={(e) => setInput({...input, [e.target.name]: e.target.value}) }
            />
            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Remember me"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
            >
              Sign In
            </Button>
            <Grid container>
              <Grid item xs>
                <Link href="#" variant="body2">
                  Forgot password?
                </Link>
              </Grid>
              <Grid item>
                <Link href="/signup">
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
};

export default LoginPage;
