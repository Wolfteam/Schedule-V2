import React from 'react';
import ReactDOM from 'react-dom';
import App from './pages/app';
import './index.css';
import * as serviceWorker from './serviceWorker';
import axios from 'axios'
import qs from 'qs';

axios.defaults.baseURL = process.env.REACT_APP_BASE_API_URL;
axios.defaults.paramsSerializer = (params) => {
  return qs.stringify(params, { arrayFormat: 'repeat' });
};

console.log(`Using url = ${axios.defaults.baseURL} as the base api url`);

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
