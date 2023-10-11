import { makeAutoObservable, runInAction } from "mobx";
import { AuthUser } from "../../models/user/user";
import { AuthUserForm } from "../../models/user/authUserForm";
import { authApi as userApi } from "../../services/api/features/flashcards/userApi";
import { setToken } from "../../services/api/core/apiConfig";

export default class UserStore {
    authUser: AuthUser | undefined = undefined;

    constructor() {
        makeAutoObservable(this);
    }

    get token(): string | undefined {
        return this.authUser?.token;
    }

    get isLoggedIn() {
        return !!this.token;
    }

    login = async (cred: AuthUserForm) => {
        try {
            const user = await userApi.login(cred);
            setToken(user.token);
            runInAction(() => this.authUser = user);
        } catch (error) {
            throw error;
        }
    }

    getUser = async (id: string) => {
        try {
            return await userApi.getUser(id);
        } catch (error) {
            throw error;
        }
    }
}