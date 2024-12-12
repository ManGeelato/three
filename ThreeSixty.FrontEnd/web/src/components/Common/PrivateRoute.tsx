import { Redirect, Route, RouteProps } from 'react-router-dom';

export const PrivateRoute = (props: RouteProps) => {
  // Check of user is logged in
  // If yes, show route
  // Otherwise, redirect to login page

  // const isLoggedIn = Boolean(localStorage.getItem('access_token'));
  const isLoggedIn = Boolean(localStorage.getItem('logged_in'));
  if (!isLoggedIn) return <Redirect to="/login" />

  return (
    <Route {...props} />
  )
}
