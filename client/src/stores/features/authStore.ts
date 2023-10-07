import { makeAutoObservable, runInAction } from "mobx";
import { User } from "../../models/user/user";
import { AuthUserForm } from "../../models/user/authUserForm";
import { authApi } from "../../services/api/features/flashcards/authApi";
import { setToken } from "../../services/api/core/apiConfig";

export default class AuthStore {
    user: User | undefined = undefined;

    constructor() {
        makeAutoObservable(this);
    }

    get token(): string | undefined {
        return this.user?.token;
    }

    get isLoggedIn() {
        return !!this.token;
    }

    login = async (cred: AuthUserForm) => {
        try {
            const user = await authApi.login(cred);
            setToken(user.token);
            runInAction(() => this.user = user);
        }
        catch (error) {
            throw error;
        }
    }
}