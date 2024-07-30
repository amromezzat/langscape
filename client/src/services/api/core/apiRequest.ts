import { IResult } from '../../../models/common/result';
import axiosInstance from './apiConfig';

function getAxiosParams(paramsMap?: Map<string, any>) {
    if (!paramsMap) {
        return undefined
    }

    const params = new URLSearchParams();
    for (const [key, value] of paramsMap) {
        params.append(key, value.toString());
    }

    return { params };
}

export const apiRequest = {
    get: <T> (url: string, queries?: Map<string, any>) => axiosInstance.get<IResult<T>>(url, getAxiosParams(queries)).then(response => response.data.data),
    post: <T> (url: string, body: {}) => axiosInstance.post<IResult<T>>(url, body).then(response => response.data.data),
    put: <T> (url: string, body: {}) => axiosInstance.put<IResult<T>>(url, body).then(response => response.data.data),
    del: <T> (url: string) => axiosInstance.delete<IResult<T>>(url).then(response => response.data.data)
}