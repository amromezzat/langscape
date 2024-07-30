import Cookies from 'js-cookie';
import getDateFromDays from '../../extensions/dateExtensions';
import { setToken } from '../../services/api/core/apiConfig';
import { makeAutoObservable } from 'mobx';

export default class AuthenticationStore {
    readonly _tokenKey: string = 'AuthenticationKey';
    readonly _expirationDateKey: string = 'AuthenticationKeyExpirationTime'
    readonly _expiryDays: number = 7;

    private _expirationDate: Date = new Date(Cookies.get(this._expirationDateKey) ?? "");

    token: string | undefined = Cookies.get(this._tokenKey);

    get isTokenValid() {
        return this._expirationDate > new Date();
    }

    constructor() {
        makeAutoObservable(this);

        if(!this.isTokenValid) {
            return;
        }

        setToken(this.token);
        setInterval(() => {
            this.token = undefined
        }, this._expirationDate.getTime());
    }

    setToken(token: string) {
        this.token = token;
        this._expirationDate = getDateFromDays(this._expiryDays);
        Cookies.set(this._tokenKey, token, { expires: this._expiryDays });
        Cookies.set(this._expirationDateKey, this._expirationDate.toISOString(), { expires: this._expiryDays });
        setToken(token);
    }

    clearToken() {
        this.token = undefined;
        this._expirationDate = new Date();
        Cookies.remove(this._tokenKey);
        Cookies.remove(this._expirationDateKey);
        setToken(undefined);
    }
}