import { AuthUserForm } from "../../../../models/user/authUserForm";
import { AuthUser, User } from "../../../../models/user/user";
import { apiRequest } from "../../core/apiRequest";

export const authApi = {
    login: (user: AuthUserForm) => apiRequest.post<AuthUser>('/accounts/login', user),
    getUser: (userId: string) => apiRequest.get<User>(`/accounts/${userId}`)
}