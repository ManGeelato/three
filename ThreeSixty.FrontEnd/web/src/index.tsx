import 'react-toastify/dist/ReactToastify.min.css';
import './index.css';

import * as serviceWorker from './serviceWorker';

// import { BrowserRouter } from 'react-router-dom';
import { CircularProgress, CssBaseline } from '@material-ui/core';
import React, { Suspense } from 'react';

import App from './App';
import { ConnectedRouter } from 'connected-react-router';
import { Provider } from 'react-redux';
import ReactDOM from 'react-dom';
import { ToastContainer } from 'react-toastify';
import { history } from 'utils';
import { store } from './app/store';

ReactDOM.render(
  <React.StrictMode>
    <Suspense fallback={<CircularProgress size="20" color="secondary" />}>
      <Provider store={store}>
        <ConnectedRouter history={history}>
          {/* <BrowserRouter> */}
          <CssBaseline>
            <App />
          </CssBaseline>
          {/* </BrowserRouter> */}
        </ConnectedRouter>

        <ToastContainer
          position="top-right"
          autoClose={5000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
        />
      </Provider>
    </Suspense>
  </React.StrictMode>,
  document.getElementById('root')
);

// incident.getElementById('root')

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
