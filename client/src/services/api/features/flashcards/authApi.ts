import { AuthUserForm } from "../../../../models/user/authUserForm";
import { User } from "../../../../models/user/user";
import { apiRequest } from "../../core/apiRequest";

export const authApi = {
    login: (user: AuthUserForm) => apiRequest.post<User>('/account/login', user)
}