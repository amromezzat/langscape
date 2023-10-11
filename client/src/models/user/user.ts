export interface User {
    username: string;
    displayName: string;
}

export interface AuthUser extends User {
    token: string;
}