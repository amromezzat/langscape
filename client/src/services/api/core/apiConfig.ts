import axios from 'axios';
import HandleError from './apiErrorHandler';

const axiosInstance = axios.create();

const sleep = async (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    });
}

let token: string | undefined = undefined;

axiosInstance.defaults.baseURL = 'http://localhost:5000/api';
axiosInstance.interceptors.request.use(config => {
    if(token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});
axiosInstance.interceptors.response.use(async response => {
    await sleep(1000);
    return response;
}, HandleError);

export function setToken(newToken: string | undefined) {
    token = newToken;
}

export default axiosInstance;