export interface User {
    username: string;
    displayName: string;
    id: string;
}

export interface AuthUser extends User {
    token: string;
}