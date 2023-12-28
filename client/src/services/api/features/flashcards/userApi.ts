import { LoginUserForm } from "../../../../models/user/loginUserForm";
import { RegisterUserForm } from "../../../../models/user/registerUserForm";
import { AuthUser, User } from "../../../../models/user/user";
import { apiRequest } from "../../core/apiRequest";

const endPoint: string = `/accounts/`;

export const authApi = {
    login: (user: LoginUserForm) => apiRequest.post<AuthUser>(endPoint + 'login', user),
    register: (user: RegisterUserForm) => apiRequest.post<AuthUser>(endPoint + 'register', user),
    getCurrentUser: () => apiRequest.get<User>(endPoint + 'current'),
    getUserById: (userId: string) => apiRequest.get<User>(endPoint + userId),
    getUserByUsername: (username: string) => apiRequest.get<User>(endPoint, new Map<string, string>([['username', username]]))
}