import { makeAutoObservable, reaction, runInAction } from "mobx";
import { User } from "../../models/user/user";
import { LoginUserForm } from "../../models/user/loginUserForm";
import { authApi as userApi } from "../../services/api/features/flashcards/userApi";
import { RegisterUserForm } from "../../models/user/registerUserForm";
import AuthenticationStore from "./authenticationStore";

export default class UserStore {
    private _authenticationStore: AuthenticationStore;
    
    user: User | undefined = undefined;
    usersRegistery = new Map<string, User>();

    constructor(authenticationStore: AuthenticationStore) {
        this._authenticationStore = authenticationStore

        makeAutoObservable(this);

        reaction(
            () => this._authenticationStore.isTokenValid,
            () => {
                if(!this._authenticationStore.isTokenValid) {
                    this.user = undefined;
                }
            }
        )
    }

    get hasActiveSession() {
        return !!this._authenticationStore.isTokenValid;
    }

    login = async (userAuth: LoginUserForm) => {
        try {
            const authenticatedUser = await userApi.login(userAuth);
            this._authenticationStore.setToken(authenticatedUser.token);
            runInAction(() => this.user = authenticatedUser);
        } catch (error) {
            throw error;
        }
    }

    register = async (userRegister: RegisterUserForm) => {
        try {
            const authenticatedUser = await userApi.register(userRegister);
            this._authenticationStore.setToken(authenticatedUser.token);
            runInAction(() => this.user = authenticatedUser);
        } catch (error) {
            throw error;
        }
    }

    logout = () => {
        this._authenticationStore.clearToken();
        this.user = undefined;
    }

    getCurrentUser = async() => {
        try {
            const user = await userApi.getCurrentUser();
            runInAction(() => this.user = user);
            return user;
        } catch (error) {
            throw error;
        }
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
        return userId === this.user?.id;
    }
}