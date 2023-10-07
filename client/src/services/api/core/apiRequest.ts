import { IResult } from '../../../models/common/result';
import axiosInstance from './apiConfig';

export const apiRequest = {
    get: <T> (url: string) => axiosInstance.get<IResult<T>>(url).then(response => response.data.data),
    post: <T> (url: string, body: {}) => axiosInstance.post<IResult<T>>(url, body).then(response => response.data.data),
    put: <T> (url: string, body: {}) => axiosInstance.put<IResult<T>>(url, body).then(response => response.data.data),
    del: <T> (url: string) => axiosInstance.delete<IResult<T>>(url).then(response => response.data.data)
}