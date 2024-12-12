import {
    CssBaseline,
    Link,
    Box,
    TextField,
    Typography,
    Grid,
    Button,
    Container,
    AppBar,
    Toolbar,
    makeStyles
  } from '@material-ui/core';
import { createTheme, ThemeProvider } from "@material-ui/core/styles";
import { useHistory } from 'react-router-dom';
import { useState } from 'react';

const theme = createTheme();

const useStyles = makeStyles((theme) => ({
    root: {
      flexGrow: 1,
    },
    title: {
      flexGrow: 1,
    },
}));

const SigninPage = () => {
    const history = useHistory();
    const classes = useStyles();

    const [input, setInput] = useState({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
    })

    const handleSignUp = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        localStorage.setItem('access_token', JSON.stringify(input));
        history.push("/login");
      };
    
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
                Sign up
              </Typography>
              <Box component="form" onSubmit={handleSignUp} sx={{ mt: 3 }}>
                <Grid container spacing={2}>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      autoComplete="given-name"
                      name="firstName"
                      required
                      fullWidth
                      id="firstName"
                      label="First Name"
                      autoFocus
                      value={input.firstName}
                      onChange={(e) => setInput({...input, [e.target.name]: e.target.value}) }
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      required
                      fullWidth
                      id="lastName"
                      label="Last Name"
                      name="lastName"
                      autoComplete="family-name"
                      value={input.lastName}
                      onChange={(e) => setInput({...input, [e.target.name]: e.target.value}) }
                    />
                  </Grid>
                  <Grid item xs={12}>
                    <TextField
                      required
                      fullWidth
                      id="email"
                      label="Email Address"
                      name="email"
                      autoComplete="email"
                      value={input.email}
                      onChange={(e) => setInput({...input, [e.target.name]: e.target.value}) }
                    />
                  </Grid>
                  <Grid item xs={12}>
                    <TextField
                      required
                      fullWidth
                      name="password"
                      label="Password"
                      type="password"
                      id="password"
                      autoComplete="new-password"
                      value={input.password}
                      onChange={(e) => setInput({...input, [e.target.name]: e.target.value}) }
                    />
                  </Grid>
                </Grid>
                <Box mt={2}>
                    <Button
                        fullWidth
                        variant="contained"
                        color="primary"
                        type="submit"
                        // onClick={handleSubmit}
                        >
                        &nbsp; Sign Up
                    </Button>
                </Box>

                <Grid container justifyContent="flex-end">
                  <Grid item>
                    <Link href="/login" variant="body2">
                      Already have an account? Sign in
                    </Link>
                  </Grid>
                </Grid>
              </Box>
            </Box>
          </Container>
        </ThemeProvider>
      );

};

export default SigninPage;
