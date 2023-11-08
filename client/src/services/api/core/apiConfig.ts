import axios from 'axios';
import HandleError from './apiErrorHandler';
import { sleep } from './utility';

const axiosInstance = axios.create();

let token: string | undefined = undefined;

axiosInstance.defaults.baseURL = 'http://localhost:5000/api';
axiosInstance.interceptors.request.use(config => {
    if(token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});
axiosInstance.interceptors.response.use(async response => {
    await sleep(1);
    return response;
}, HandleError);

export function setToken(newToken: string | undefined) {
    token = newToken;
}

export default axiosInstance;