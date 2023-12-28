import { makeAutoObservable, runInAction } from "mobx";
import { AuthUser, User } from "../../models/user/user";
import { AuthUserForm } from "../../models/user/authUserForm";
import { authApi as userApi } from "../../services/api/features/flashcards/userApi";
import { setToken } from "../../services/api/core/apiConfig";
import { RegisterUserForm } from "../../models/user/registerUserForm";

export default class UserStore {
    authUser: AuthUser | undefined = undefined;
    usersRegistery = new Map<string, User>();

    constructor() {
        makeAutoObservable(this);
    }

    get token(): string | undefined {
        return this.authUser?.token;
    }

    get isLoggedIn() {
        return !!this.token;
    }

    login = async (userAuth: AuthUserForm) => {
        try {
            const user = await userApi.login(userAuth);
            setToken(user.token);
            runInAction(() => this.authUser = user);
        } catch (error) {
            throw error;
        }
    }

    register = async (userRegister: RegisterUserForm) => {
        try {
            const user = await userApi.register(userRegister);
            setToken(user.token);
            runInAction(() => this.authUser = user);
        } catch (error) {
            throw error;
        }
    }

    logout = () => {
        setToken(undefined);
        this.authUser = undefined;
    }

    getUserById = async (id: string) => {
        try {
            const user = await userApi.getUserById(id);
            runInAction(() => this.usersRegistery.set(user.username, user));
            return user;
        } catch (error) {
            throw error;
        }
    }

    getUserByUsername = async (username: string) => {
        try {
            const user = await userApi.getUserByUsername(username);
            runInAction(() => this.usersRegistery.set(user.username, user));
            return user;
        } catch (error) {
            throw error;
        }
    }

    isCurrentUser = (userId: string) => {
        return userId === this.authUser?.id;
    }
}