import axios, { AxiosRequestConfig, AxiosResponse } from "axios";

const axiosClient =  axios.create({
  // http://20.87.210.199/ThreeSixty.Api/api
  // https://localhost:7244/api
  baseURL: 'https://localhost:7244/api',
  headers: {
    'Content-Type': 'application/json'
  }
});

// Interceptors
axiosClient.interceptors.request.use(function (config: AxiosRequestConfig) {  
  return config;
}, function (error) {  
  return Promise.reject(error);
});

axiosClient.interceptors.response.use(function (response: AxiosResponse) {
  // status code 2xx 
  return response.data;
}, function (error) {
  // status codes that outside 2xx 
  return Promise.reject(error);
});

export default axiosClient;