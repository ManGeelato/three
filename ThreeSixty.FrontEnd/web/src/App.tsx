import { NotFound, PrivateRoute } from 'components/Common';
import { Redirect, Route, Switch } from 'react-router-dom';
import { AdminLayout } from './components/Layout';
import LoginPage from 'features/auth/pages/LoginPage';
import SigninPage from 'features/auth/pages/SignupPage';

function App() {
  return (
    <>
      <Switch>
        <Redirect exact from="/" to="/admin/dashboard" />
        <Route path="/login">
          <LoginPage />
        </Route>

        <Route path="/signup">
          <SigninPage />
        </Route>

        <PrivateRoute path="/admin">
          <AdminLayout />
        </PrivateRoute>

        <Route>
          <NotFound />
        </Route>
      </Switch>
    </>
  );
}

export default App;
