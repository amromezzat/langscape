import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../routes/Routes";
import { FlashCardSet } from "../models/flashCards/flashCardSet";
import { IResult } from "../models/common/result";

const sleep = async (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    });
}

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.response.use(async response => {
    await sleep(1000);
    return response;
}, (error: AxiosError) => {
    const {data, status, config} = error.response as AxiosResponse;
    switch(status){
        case 400:
            if(config.method === 'get' && data.errors.hasOwnProperty('id')) {
                router.navigate('/not-found')
            }
            if(data.errors) {
                const modalStateErrors = [];
                for(const key in data.errors) {
                    if(data.errors[key]) {
                        modalStateErrors.push(data.errors[key]);
                    }
                }
                throw modalStateErrors.flat();
            } else {
                toast.error(data);
            }
            break;
        case 401:
            toast.error('unauthorised');
            break;
        case 403:
            toast.error('forbidden');
            break;
        case 404:
            router.navigate('/not-found');
            break;
        case 500:
            router.navigate('/server-error');
            break;
    }
    return Promise.reject(error);
});

const requests = {
    get: <T> (url: string) => axios.get<IResult<T>>(url).then(response => response.data.data),
    post: <T> (url: string, body: {}) => axios.post<IResult<T>>(url, body).then(response => response.data.data),
    put: <T> (url: string, body: {}) => axios.put<IResult<T>>(url, body).then(response => response.data.data),
    del: <T> (url: string) => axios.delete<IResult<T>>(url).then(response => response.data.data)
}

const FlashCards = {
    get: () => requests.get<FlashCardSet[]>('/flashcards')
}

const agent = {
    FlashCards
}

export default agent;