import { AxiosError, AxiosResponse } from "axios";
import { router } from "../../../routes/Routes";
import { toast } from "react-toastify";

export default function HandleError(error: AxiosError) {
    console.log(error);
    const {data, status, config} = error.response as AxiosResponse;
    switch(status) {
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
}